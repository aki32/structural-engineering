

namespace Dynamics.ElastoplasticAnalysis;
public class ElasticTrilinearModel : ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    public double K2;
    public double K3;

    public double Fy1;
    public double Fy2;

    public double Xy1 = 0d;
    public double Xy2 = 0d;

    // ★★★★★★★★★★★★★★★ inits

    public ElasticTrilinearModel(double K1, double beta1, double Fy1, double beta2, double Fy2)
    {
        this.K1 = K1;
        this.K2 = K1 * beta1;
        this.K3 = K1 * beta2;
        this.Fy1 = Fy1;
        this.Fy2 = Fy2;

        Xy1 = Fy1 / K1;
        Xy2 = Xy1 + (Fy2 - Fy1) / K2;
    }

    // ★★★★★★★★★★★★★★★ methods

    public override double TryCalcNextF(double targetX)
    {
        if (CurrentX == targetX)
            return NextF;

        NextX = targetX;

        #region fを求める

        // 設計イラストの通り
        var f1 = K1 * NextX ;
        var f2 = K2 * (NextX - Xy1) + ((NextX > 0) ? Fy1 : -Fy1);
        var f3 = K3 * (NextX - Xy2) + ((NextX > 0) ? Fy2 : -Fy2);

        // 最小値／最大値
        var fs = new List<double> { f1, f2, f3 };
        if (NextX > 0)
            NextF = fs.Min();
        else
            NextF = fs.Max();

        #endregion

        return NextF;
    }

    // ★★★★★★★★★★★★★★★

}
