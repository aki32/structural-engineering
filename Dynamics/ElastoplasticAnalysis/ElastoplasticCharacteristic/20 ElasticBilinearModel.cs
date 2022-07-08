

namespace Dynamics.ElastoplasticAnalysis;
public class ElasticBilinearModel : ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    public double K2;

    public double Fy;

    public double Xy = 0d;

    // ★★★★★★★★★★★★★★★ inits

    public ElasticBilinearModel(double K1, double beta, double Fy)
    {
        this.K1 = K1;
        this.K2 = K1 * beta;
        this.Fy = Fy;

        Xy = Fy / K1;
    }

    // ★★★★★★★★★★★★★★★ methods

    public override double TryCalcNextF(double targetX)
    {
        if (CurrentX == targetX)
            return NextF;

        NextX = targetX;

        #region fを求める

        // 設計イラストの通り
        var f1 = K1 * NextX;
        var f2 = K2 * (NextX - Xy) + ((NextX > 0) ? Fy : -Fy);

        // max, min
        var fs = new List<double> { f1, f2 };
        if (NextX > 0)
            NextF = fs.Min();
        else
            NextF = fs.Max();

        #endregion

        return NextF;
    }

    // ★★★★★★★★★★★★★★★

}
