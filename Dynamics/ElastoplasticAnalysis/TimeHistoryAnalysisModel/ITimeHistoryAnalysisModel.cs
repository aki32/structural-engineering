using Aki32_Utilities.OwesomeModels;

namespace Dynamics.ElastoplasticAnalysis;
public interface ITimeHistoryAnalysisModel
{
    
    // ★★★★★★★★★★★★★★★ methods

    public TimeHistory Calc(SDoFModel model, TimeHistory wave);

    // ★★★★★★★★★★★★★★★ 

}
