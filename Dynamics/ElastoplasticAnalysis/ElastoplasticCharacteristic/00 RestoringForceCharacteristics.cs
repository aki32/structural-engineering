

namespace Dynamics.ElastoplasticAnalysis;
public abstract class ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    /// <summary>
    /// 初期剛性
    /// </summary>
    public double K1;

    /// <summary>
    /// 次の変位に対する応力を算出
    /// </summary>
    /// <param name="targetX"></param>
    /// <returns></returns>
    public abstract double CalcNextF(double targetX);

    /// <summary>
    /// 現在の接線の傾き
    /// </summary>
    public double CurrentK
    {
        get
        {
            var dX = LastX - CurrentX;
            var dF = LastF - CurrentF;
            if (dX == 0)
                return K1;
            else
                return Math.Max(K1, Math.Abs(dF / dX));
        }
    }

    public double CurrentX;
    public double CurrentF;
    public double LastX;
    public double LastF;

    // ★★★★★★★★★★★★★★★

}
