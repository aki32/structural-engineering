

namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public class BilinearModel : RestoringForceCharacteristics
{

    // ★★★★★★★★★★★★★★★ props

    public double beta { get; set; }
    public double Fy { get; set; }

    public double K2 => K1 * beta;

    private double Xy = 0d;

    // ★★★★★★★★★★★★★★★ inits

    public BilinearModel(double K1, double beta, double Fy)
    {
        this.K1 = K1;
        this.beta = beta;
        this.Fy = Fy;

        Xy = Fy / K1;
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
        var f1 = K1 * dX + LastF;
        var fy = K2 * (CurrentX - Xy) + ((CurrentX > LastX) ? Fy : -Fy);

        // 最小値／最大値
        var fs = new List<double> { f1, fy };
        if (CurrentX > LastX)
            CurrentF = fs.Min();
        else
            CurrentF = fs.Max();

        #endregion

        return CurrentF;
    }

    // ★★★★★★★★★★★★★★★

}
