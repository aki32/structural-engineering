

namespace Dynamics.Class.ElastoplasticAnalysis;
public class ElasticModel : RestoringForceCharacteristic
{

    // ★★★★★★★★★★★★★★★ inits

    public ElasticModel(double K1)
    {
        this.K1 = K1;
    }

    // ★★★★★★★★★★★★★★★ methods

    public override double CalcNextF(double targetX)
    {
        if (LastX == targetX)
            return CurrentF;

        LastX = CurrentX;
        LastF = CurrentF;
        CurrentX = targetX;
        CurrentF = targetX * K1;
        
        return CurrentF;
    }

    // ★★★★★★★★★★★★★★★ 

}
