using Dynamics.Class.ElastoplasticAnalysis;
using Dynamics.Class.ElastoplasticAnalysis;
using Aki32_Utilities.OwesomeModels;

namespace Dynamics.Class.ElastoplasticAnalysis;
public interface ITimeHistoryAnalysisModel
{
    
    // ★★★★★★★★★★★★★★★ methods

    public TimeHistory Calc(SDoFModel model, TimeHistory wave);

    // ★★★★★★★★★★★★★★★ 

}
