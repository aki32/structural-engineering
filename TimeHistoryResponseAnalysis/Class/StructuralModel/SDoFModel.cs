using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
using Aki32_Utilities.OwesomeModels;

namespace TimeHistoryResponseAnalysis.Class.StructuralModel;
public class SDoFModel
{

    // ★★★★★★★★★★★★★★★ プロパティたち

    public RestoringForceCharacteristics.RestoringForceCharacteristics RFC { get; set; }

    public double h { get; set; }
    public double m { get; set; }

    public double w => Math.Sqrt(RFC.CurrentK / m);
    public double T => 2 * Math.PI / w;

    public double wo;
    public double To;

    // ★★★★★★★★★★★★★★★ 初期化

    #region initializers

    private SDoFModel(double m, double h, RestoringForceCharacteristics.RestoringForceCharacteristics rFC)
    {
        RFC = rFC;

        this.h = h;
        this.m = m;

        wo = w;
        To = T;
    }
    public static SDoFModel FromM(double m, double h = 0, RestoringForceCharacteristics.RestoringForceCharacteristics? rfc = null)
    {
        if (rfc == null)
            rfc = new ElasticModel(1);
        return new SDoFModel(m, h, rfc);
    }
    public static SDoFModel FromT(double T, double h = 0, RestoringForceCharacteristics.RestoringForceCharacteristics? rfc = null)
    {
        if (rfc == null)
            rfc = new ElasticModel(1);
        var initialwo = 2 * Math.PI / T;
        var m = rfc.K1 / (initialwo * initialwo);
        return new SDoFModel(m, h, rfc);
    }

    #endregion

    // ★★★★★★★★★★★★★★★ メソッド

    public TimeHistory Calc(TimeHistory wave, ITimeHistoryAnalysisModel thaModel)
    {
        return thaModel.Calc(this, wave);
    }

    // ★★★★★★★★★★★★★★★

}
