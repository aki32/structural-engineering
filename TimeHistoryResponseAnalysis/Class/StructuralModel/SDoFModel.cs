using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
using Aki32_Utilities.OwesomeModels;

namespace TimeHistoryResponseAnalysis.Class.StructuralModel;
public class SDoFModel
{
    public double T { get; set; }
    public double h { get; set; }
    public IRestoringForceCharacteristics RFC { get; set; }

    public SDoFModel(double T, double h = 0, IRestoringForceCharacteristics rfc = null)
    {
        this.T = T;
        this.h = h;
        RFC = rfc;
    }

    public TimeHistory Calc(TimeHistory wave, ITimeHistoryAnalysisModel thaModel)
    {
        return thaModel.Calc(this, wave);
    }
}
