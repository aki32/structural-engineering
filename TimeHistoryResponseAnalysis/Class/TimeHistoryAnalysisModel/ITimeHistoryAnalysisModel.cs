using TimeHistoryResponseAnalysis.Class.ElastoPlasticModel;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel
{
    internal interface ITimeHistoryAnalysisModel
    {
        internal TimeHistory Calc(SDoFModel model, TimeHistory wave);
    }
}
