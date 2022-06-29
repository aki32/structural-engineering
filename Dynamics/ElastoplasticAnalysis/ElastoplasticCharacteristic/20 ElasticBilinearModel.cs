

namespace Dynamics.ElastoplasticAnalysis;
public class ElasticBilinearModel : ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    public double Fy { get; set; }

    public double K2 { get; set; }

    private double Xy = 0d;

    // ★★★★★★★★★★★★★★★ inits

    public ElasticBilinearModel(double K1, double beta, double Fy)
    {
        this.K1 = K1;
        this.K2 = K1 * beta;
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
        var f1 = K1 * CurrentX;
        var f2 = K2 * (CurrentX - Xy) + ((CurrentX > 0) ? Fy : -Fy);

        // max, min
        var fs = new List<double> { f1, f2 };
        if (CurrentX > 0)
            CurrentF = fs.Min();
        else
            CurrentF = fs.Max();

        #endregion

        return CurrentF;
    }

    // ★★★★★★★★★★★★★★★

}
