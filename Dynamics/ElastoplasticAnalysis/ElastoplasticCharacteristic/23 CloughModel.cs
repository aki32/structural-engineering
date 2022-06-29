

namespace Dynamics.ElastoplasticAnalysis;
public class CloughModel : ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    public double Fy { get; set; }

    public double K2 { get; set; }

    private double Xy = 0d;
    private double MaxF = 0d;
    private double MaxX = 0d;
    private double MinF = 0d;
    private double MinX = 0d;

    // ★★★★★★★★★★★★★★★ inits

    public CloughModel(double K1, double beta, double Fy)
    {
        this.K1 = K1;
        this.K2 = K1 * beta;
        this.Fy = Fy;

        Xy = Fy / K1;
        MaxF = Fy;
        MaxX = MaxF / K1;
        MinF = -Fy;
        MinX = MinF / K1;
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
        var f1r = K1 * dX + LastF;
        var f2 = K2 * (CurrentX - Xy) + ((CurrentX > LastX) ? Fy : -Fy);
        double fc; // fc0と兼用

        // 向かってる先でX軸をまたがない／またぐ
        if ((CurrentX > LastX && LastF > 0) || (CurrentX < LastX && LastF < 0))
        {
            // fc
            if (CurrentX > LastX)
                fc = CalcF_FromPoints(LastX, LastF, MaxX, MaxF, CurrentX);
            else
                fc = CalcF_FromPoints(LastX, LastF, MinX, MinF, CurrentX);
        }
        else
        {
            // fc0
            var HitX = LastX + (-LastF / K1);

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
        double maxK = K1;
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
