using TimeHistoryResponseAnalysis.Class.ElastoPlasticModel;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel
{
    public interface ITimeHistoryAnalysisModel
    {
        public TimeHistory Calc(SDoFModel model, TimeHistory wave);
    }
}
