

namespace Dynamics.ElastoplasticAnalysis;
public class PerfectElastoPlasticModel : BilinearModel
{

    // ★★★★★★★★★★★★★★★ inits

    public PerfectElastoPlasticModel(double beta, double Fy) : base(1e10, beta, Fy)
    {
        // K1は，X が発散しないくらいの適度に大きな数値とする。
    }

    // ★★★★★★★★★★★★★★★

}
