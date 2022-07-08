

namespace Dynamics.ElastoplasticAnalysis;
public class DegradingCloughModel : ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    public double K1r => K1 * Math.Pow(Math.Max(1, Math.Abs(DegradeStartX / Xy)), -alpha); // DegradingStateにいない時は自動的に K1 となる。
    public double K2;

    public double Fy;
    public double alpha;

    public double Xy = 0d;
    private double MaxF = 0d;
    private double MaxX = 0d;
    private double MinF = 0d;
    private double MinX = 0d;

    public bool IsInDegradingState = false;
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
        if (CurrentX == targetX)
            return NextF;

        CurrentX = NextX;
        CurrentF = NextF;
        NextX = targetX;

        #region fを求める

        // 設計イラストの通り
        var dX = NextX - CurrentX;
        var f1r = K1r * dX + CurrentF;
        var f2 = K2 * (NextX - Xy) + ((NextX > CurrentX) ? Fy : -Fy);
        double fc; // fcrなどと兼用

        // 向かってる先でX軸をまたがない／またぐ
        if ((NextX > CurrentX && CurrentF > 0) || (NextX < CurrentX && CurrentF < 0))
        {
            // もし DegradingState に居るのにX軸をまたがないなら，fcをセットバックさせる。
            if (IsInDegradingState)
            {
                // fcr
                if (NextX > CurrentX)
                    fc = CalcF_FromPoints(DegradeStartX, DegradeStartF, MaxX, MaxF, NextX);
                else
                    fc = CalcF_FromPoints(DegradeStartX, DegradeStartF, MinX, MinF, NextX);
            }
            else
            {
                // fc
                if (NextX > CurrentX)
                    fc = CalcF_FromPoints(CurrentX, CurrentF, MaxX, MaxF, NextX);
                else
                    fc = CalcF_FromPoints(CurrentX, CurrentF, MinX, MinF, NextX);
            }
        }
        else
        {
            double HitX = CurrentX + (-CurrentF / K1r);

            // fc0, fcr0
            if (NextX > CurrentX)
                fc = CalcF_FromPoints(HitX, 0, MaxX, MaxF, NextX);
            else
                fc = CalcF_FromPoints(HitX, 0, MinX, MinF, NextX);
        }

        // 最小値／最大値
        var fs = new List<double> { f1r, f2, fc };
        if (NextX > CurrentX)
            NextF = fs.Min();
        else
            NextF = fs.Max();

        // もし fr を採用した場合は，DegradingState に居ることになる。そうでなければ DegradingState 外
        if (f1r == NextF)
        {
            if (IsInDegradingState == false)
            {
                DegradeStartX = NextX;
                DegradeStartF = NextF;
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

        if (NextX > MaxX)
        {
            MaxX = NextX;
            MaxF = NextF;
        }
        else if (NextX < MinX)
        {
            MinX = NextX;
            MinF = NextF;
        }

        #endregion

        return NextF;
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
