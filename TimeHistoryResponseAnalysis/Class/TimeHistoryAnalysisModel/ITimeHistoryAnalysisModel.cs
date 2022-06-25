using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using Aki32_Utilities.OwesomeModels;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
public interface ITimeHistoryAnalysisModel
{
    
    // ★★★★★★★★★★★★★★★ methods

    public TimeHistory Calc(SDoFModel model, TimeHistory wave);

    // ★★★★★★★★★★★★★★★ 

}
