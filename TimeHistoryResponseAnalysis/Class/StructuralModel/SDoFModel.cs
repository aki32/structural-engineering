using TimeHistoryResponseAnalysis.Class.ElastoPlasticModel;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis.Class.StructuralModel
{
    internal class SDoFModel
    {
        public double T { get; set; }
        public double h { get; set; }
        public IElastoPlasticModel epmodel { get; set; }

        public SDoFModel(double T, double h = 0, IElastoPlasticModel epmodel = null)
        {
            this.T = T;
            this.h = h;
            this.epmodel = epmodel;
        }

        public TimeHistory Calc(TimeHistory wave, ITimeHistoryAnalysisModel thaModel)
        {
            return thaModel.Calc(this, wave);
        }
    }
}
