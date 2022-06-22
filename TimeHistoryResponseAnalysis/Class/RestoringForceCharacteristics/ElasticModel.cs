using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public class ElasticModel : IRestoringForceCharacteristics
{
    public double K1 { get; set; }
    
    public ElasticModel(double K1)
    {
        this.K1 = K1;
    }
    
    double IRestoringForceCharacteristics.CalcNextF(double targetX)
    {
        return K1 * targetX;
    }

}
