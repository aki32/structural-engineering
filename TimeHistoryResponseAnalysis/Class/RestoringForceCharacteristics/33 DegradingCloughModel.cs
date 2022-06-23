
namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public class DegradingCloughModel : RestoringForceCharacteristics
{

    #region ★★★★★★★★★★★★★★★ プロパティたち

    public double beta { get; set; }
    public double Fy { get; set; }
    public double alpha { get; set; }
    public double K2 => K1 * beta;

    private double MaxF = 0d;
    private double MaxX = 0d;
    private double MinF = 0d;
    private double MinX = 0d;

    private bool IsInDegradingState = false;
    private bool IsDegradingStatePositive = false;
    private double Xr_Start = 0;
    private double Kr = 0;

    #endregion

    public DegradingCloughModel(double K1, double beta, double Fy, double alpha)
    {
        this.K1 = K1;
        this.beta = beta;
        this.Fy = Fy;
        this.alpha = alpha;

        MaxF = Fy;
        MaxX = MaxF / K1;
        MinF = -Fy;
        MinX = MinF / K1;
    }

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
        var f1 = K1 * dX + LastF;
        var fy = K2 * CurrentX + ((CurrentX > LastX) ? Fy : -Fy);
        double fc; // fc0と兼用

        // 向かってる先でX軸をまたがない／またぐ
        if ((CurrentX > LastX && LastF > 0) || (CurrentX < LastX && LastF < 0))
        {
            if (CurrentX > LastX)
                fc = GetF(LastX, LastF, MaxX, MaxF, CurrentX);
            else
                fc = GetF(LastX, LastF, MinX, MinF, CurrentX);
        }
        else
        {
            var HitX = LastX + (-LastF / K1);

            if (CurrentX > LastX)
                fc = GetF(HitX, 0, MaxX, MaxF, CurrentX);
            else
                fc = GetF(HitX, 0, MinX, MinF, CurrentX);
        }

        // 最小値／最大値
        var fs = new List<double> { f1, fy, fc };
        if (CurrentX > LastX)
            CurrentF = fs.Min();
        else
            CurrentF = fs.Max();

        #endregion

        UpdateMinMax();
        return CurrentF;
    }

    private void UpdateMinMax()
    {
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
    double GetF(double X1, double F1, double X2, double Y2, double targetX)
    {
        double Kc;
        if (X1 == X2)
            Kc = K2;
        else
            Kc = (Y2 - F1) / (X2 - X1);
        Kc = Math.Min(K1, Kc); // for safety
        return Kc * (targetX - X1) + F1;
    }

}
