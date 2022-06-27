
namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public class ElasticTetralinearModel : RestoringForceCharacteristic
{
    
    // ★★★★★★★★★★★★★★★ props

    public double Fy1 { get; set; }
    public double Fy2 { get; set; }
    public double Fy3 { get; set; }

    public double K2;
    public double K3;
    public double K4;

    private double Xy1 = 0d;
    private double Xy2 = 0d;
    private double Xy3 = 0d;

    // ★★★★★★★★★★★★★★★ inits

    public ElasticTetralinearModel(double K1, double beta1, double Fy1, double beta2, double Fy2, double beta3, double Fy3)
    {
        this.K1 = K1;
        this.K2 = K1 * beta1;
        this.K3 = K1 * beta2;
        this.K4 = K1 * beta3;
        this.Fy1 = Fy1;
        this.Fy2 = Fy2;
        this.Fy3 = Fy3;

        Xy1 = Fy1 / K1;
        Xy2 = Xy1 + (Fy2 - Fy1) / K2;
        Xy3 = Xy2 + (Fy3 - Fy2) / K3;
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
        var f1 = K1 * CurrentX ;
        var f2 = K2 * (CurrentX - Xy1) + ((CurrentX > 0) ? Fy1 : -Fy1);
        var f3 = K3 * (CurrentX - Xy2) + ((CurrentX > 0) ? Fy2 : -Fy2);
        var f4 = K4 * (CurrentX - Xy3) + ((CurrentX > 0) ? Fy3 : -Fy3);

        // 最小値／最大値
        var fs = new List<double> { f1, f2, f3, f4 };
        if (CurrentX > 0)
            CurrentF = fs.Min();
        else
            CurrentF = fs.Max();

        #endregion

        return CurrentF;
    }

    // ★★★★★★★★★★★★★★★

}
