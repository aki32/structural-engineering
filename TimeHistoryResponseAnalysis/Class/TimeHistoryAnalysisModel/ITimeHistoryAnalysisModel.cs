using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using Aki32_Utilities.OwesomeModels.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
public interface ITimeHistoryAnalysisModel
{
    public TimeHistory Calc(SDoFModel model, TimeHistory wave);
}
