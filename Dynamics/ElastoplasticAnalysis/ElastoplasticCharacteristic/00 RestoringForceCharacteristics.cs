

namespace Dynamics.ElastoplasticAnalysis;
public abstract class ElastoplasticCharacteristic
{

    // ★★★★★★★★★★★★★★★ props

    /// <summary>
    /// 初期剛性
    /// </summary>
    public double K1;

    public double NextX;
    public double NextF;
    public double NextAverageK
    {
        get
        {
            var dX = NextX - CurrentX;
            var dF = NextF - CurrentF;
            if (dX == 0)
                return CurrentK;
            else
                return Math.Max(Math.Min(K1, Math.Abs(dF / dX)), MIN_K);
        }
    }
    public double CurrentX;
    public double CurrentF;
    public double CurrentK;

    // ★★★★★★★★★★★★★★★ methods

    /// <summary>
    /// 次の変位に対する応力を算出してみる。
    /// </summary>
    /// <param name="targetX"></param>
    /// <returns></returns>
    public abstract double TryCalcNextF(double nextX);

    /// <summary>
    /// NextX, NextFの組み合わせを採用。
    /// </summary>
    public virtual void AdoptNextPoint()
    {
        CurrentK = NextAverageK;
        CurrentX = NextX;
        CurrentF = NextF;
    }


    // ★★★★★★★★★★★★★★★ const

    private const double MIN_K = 1e-10;


    // ★★★★★★★★★★★★★★★

}
