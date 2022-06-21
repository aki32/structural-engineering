using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using Utility.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
public interface ITimeHistoryAnalysisModel
{
    public TimeHistory Calc(SDoFModel model, TimeHistory wave);
}
