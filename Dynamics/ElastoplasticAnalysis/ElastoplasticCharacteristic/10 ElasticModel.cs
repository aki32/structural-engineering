

namespace Dynamics.ElastoplasticAnalysis;
public class ElasticModel : ElastoplasticCharacteristic
{
    
    // ★★★★★★★★★★★★★★★ props

    // ★★★★★★★★★★★★★★★ inits

    public ElasticModel(double K1)
    {
        this.K1 = K1;

        CurrentK = K1;
    }

    // ★★★★★★★★★★★★★★★ methods

    public override double TryCalcNextF(double targetX)
    {
        if (CurrentX == targetX)
            return NextF;

        NextX = targetX;
        NextF = targetX * K1;
        
        return NextF;
    }

    // ★★★★★★★★★★★★★★★ 

}
