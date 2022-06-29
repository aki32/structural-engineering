

namespace Dynamics.ElastoplasticAnalysis;
public class DegradingCloughModel : ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    public double Fy { get; set; }
    public double alpha { get; set; }

    public double K2 { get; set; }
    public double K1r => K1 * Math.Pow(Math.Max(1, Math.Abs(DegradeStartX / Xy)), -alpha); // DegradingStateにいない時は自動的に K1 となる。

    private double Xy = 0d;
    private double MaxF = 0d;
    private double MaxX = 0d;
    private double MinF = 0d;
    private double MinX = 0d;

    private bool IsInDegradingState = false;
    private double DegradeStartX = 0;
    private double DegradeStartF = 0;

    // ★★★★★★★★★★★★★★★ inits

    public DegradingCloughModel(double K1, double beta, double Fy, double alpha)
    {
        this.K1 = K1;
        this.K2 = K1 * beta;
        this.Fy = Fy;
        this.alpha = alpha;

        Xy = Fy / K1;
        MaxF = Fy;
        MaxX = Fy / K1;
        MinF = -Fy;
        MinX = -Fy / K1;
    }

    // ★★★★★★★★★★★★★★★ methods

    public override double CalcNextF(double targetX)
    {
        if (LastX == targetX)
            return CurrentF;

        LastX = CurrentX;
        LastF = CurrentF;
        CurrentX = targetX;

        #region fを求める

        // 設計イラストの通り
        var dX = CurrentX - LastX;
        var f1r = K1r * dX + LastF;
        var f2 = K2 * (CurrentX - Xy) + ((CurrentX > LastX) ? Fy : -Fy);
        double fc; // fcrなどと兼用

        // 向かってる先でX軸をまたがない／またぐ
        if ((CurrentX > LastX && LastF > 0) || (CurrentX < LastX && LastF < 0))
        {
            // もし DegradingState に居るのにX軸をまたがないなら，fcをセットバックさせる。
            if (IsInDegradingState)
            {
                // fcr
                if (CurrentX > LastX)
                    fc = CalcF_FromPoints(DegradeStartX, DegradeStartF, MaxX, MaxF, CurrentX);
                else
                    fc = CalcF_FromPoints(DegradeStartX, DegradeStartF, MinX, MinF, CurrentX);
            }
            else
            {
                // fc
                if (CurrentX > LastX)
                    fc = CalcF_FromPoints(LastX, LastF, MaxX, MaxF, CurrentX);
                else
                    fc = CalcF_FromPoints(LastX, LastF, MinX, MinF, CurrentX);
            }
        }
        else
        {
            double HitX = LastX + (-LastF / K1r);

            // fc0, fcr0
            if (CurrentX > LastX)
                fc = CalcF_FromPoints(HitX, 0, MaxX, MaxF, CurrentX);
            else
                fc = CalcF_FromPoints(HitX, 0, MinX, MinF, CurrentX);
        }

        // 最小値／最大値
        var fs = new List<double> { f1r, f2, fc };
        if (CurrentX > LastX)
            CurrentF = fs.Min();
        else
            CurrentF = fs.Max();

        // もし fr を採用した場合は，DegradingState に居ることになる。そうでなければ DegradingState 外
        if (f1r == CurrentF)
        {
            if (IsInDegradingState == false)
            {
                DegradeStartX = CurrentX;
                DegradeStartF = CurrentF;
                IsInDegradingState = true;
            }
        }
        else
        {
            DegradeStartX = 0;
            DegradeStartF = 0;
            IsInDegradingState = false;
        }

        #endregion


        #region 最大最小を更新

        if (CurrentX > MaxX)
        {
            MaxX = CurrentX;
            MaxF = CurrentF;
        }
        else if (CurrentX < MinX)
        {
            MinX = CurrentX;
            MinF = CurrentF;
        }

        #endregion

        return CurrentF;
    }

    /// <summary>
    /// (X1,F1),(X2,F2)の2点を通過する直線上の targetX での F を返す。
    /// </summary>
    /// <param name="X1"></param>
    /// <param name="F1"></param>
    /// <param name="X2"></param>
    /// <param name="Y2"></param>
    /// <param name="targetX"></param>
    /// <returns></returns>
    private double CalcF_FromPoints(double X1, double F1, double X2, double Y2, double targetX)
    {
        double maxK = K1r;
        double Kc;
        if (X1 == X2)
            Kc = maxK; // 最大にしておくことで，min で最終的に選ばれなくなる。
        else
            Kc = (Y2 - F1) / (X2 - X1);
        Kc = Math.Min(maxK, Kc); // for safety
        return Kc * (targetX - X1) + F1;
    }

    // ★★★★★★★★★★★★★★★

}
