

namespace Dynamics.ElastoplasticAnalysis;
public class BilinearModel : ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    public double K2;

    // ★★★★★★★★★★★★★★★ inits

    public BilinearModel(double K1, double beta, double Fy)
    {
        this.beta = beta;
        this.K1 = K1;
        this.K2 = K1 * beta;
        this.Fy1 = Fy;

        Xy1 = Fy / K1;

        CurrentK = K1;
    }

    // ★★★★★★★★★★★★★★★ methods

    public override double TryCalcNextF(double nextX)
    {
        if (CurrentX == nextX)
            return CurrentF;

        NextX = nextX;

        #region fを求める

        // 設計イラストの通り
        var dX = NextX - CurrentX;
        var f1 = K1 * dX + CurrentF;
        var fy = K2 * (NextX - Xy1) + ((NextX > CurrentX) ? Fy1 : -Fy1);

        // 最小値／最大値
        var fs = new List<double> { f1, fy };
        if (NextX > CurrentX)
            NextF = fs.Min();
        else
            NextF = fs.Max();

        #endregion

        return NextF;
    }

    // ★★★★★★★★★★★★★★★

}
