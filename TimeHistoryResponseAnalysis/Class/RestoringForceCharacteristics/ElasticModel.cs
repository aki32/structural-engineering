using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public class ElasticModel : RestoringForceCharacteristics
{
    public ElasticModel(double K1)
    {
        this.K1 = K1;
    }

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
}
