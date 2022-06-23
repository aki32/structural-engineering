
namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public class DegradingCloughModel : RestoringForceCharacteristics
{

    #region ★★★★★★★★★★★★★★★ プロパティたち

    public double beta { get; set; }
    public double Fy { get; set; }
    public double alpha { get; set; }

    public double K2 => K1 * beta;
    public double Kr => K1 * Math.Pow(Math.Abs(Math.Max(1, DegradeStartX / Xy)), -alpha);

    private double Xy = 0d;
    private double MaxF = 0d;
    private double MaxX = 0d;
    private double MinF = 0d;
    private double MinX = 0d;

    private bool IsInDegradingState = false;
    private double DegradeStartX = 0;
    private double DegradeStartF = 0;

    #endregion

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="K1"></param>
    /// <param name="beta"></param>
    /// <param name="Fy"></param>
    /// <param name="alpha">
    /// RC : commonly, 0.4 - 0.5
    /// </param>
    public DegradingCloughModel(double K1, double beta, double Fy, double alpha)
    {
        this.K1 = K1;
        this.beta = beta;
        this.Fy = Fy;
        this.alpha = alpha;

        Xy = Fy / K1;
        MaxF = Fy;
        MaxX = Fy / K1;
        MinF = -Fy;
        MinX = -Fy / K1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetX"></param>
    /// <returns></returns>
    public override double CalcNextF(double targetX)
    {
        if (LastX == targetX)
            return CurrentF;

        LastX = CurrentX;
        LastF = CurrentF;
        CurrentX = targetX;

        #region fを求める


        if (CurrentX >= 40)
        {

        }


        // 設計イラストの通り
        var dX = CurrentX - LastX;
        var fr = Kr * dX + LastF;
        var fy = K2 * (CurrentX - Xy) + ((CurrentX > LastX) ? Fy : -Fy);
        double fc; // fcrと兼用

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
            // fc0
            var HitX = LastX + (-LastF / K1);

            if (CurrentX > LastX)
                fc = CalcF_FromPoints(HitX, 0, MaxX, MaxF, CurrentX);
            else
                fc = CalcF_FromPoints(HitX, 0, MinX, MinF, CurrentX);
        }

        // 最小値／最大値
        var fs = new List<double> { fr, fy, fc };
        if (CurrentX > LastX)
            CurrentF = fs.Min();
        else
            CurrentF = fs.Max();

        // もし fr を採用した場合は，DegradingState に居ることになる。そうでなければ DegradingState 外
        if (fr == CurrentF)
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
            IsInDegradingState = false;
        }

        #endregion

        UpdateMinMax();
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
        double Kc;
        if (X1 == X2)
            Kc = K2;
        else
            Kc = (Y2 - F1) / (X2 - X1);
        Kc = Math.Min(K1, Kc); // for safety
        return Kc * (targetX - X1) + F1;
    }

    /// <summary>
    /// 
    /// </summary>
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

}
