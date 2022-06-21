using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using Utility.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
public class NigamJenningsModel : ITimeHistoryAnalysisModel
{
    /// <summary>
    /// constructor
    /// </summary>
    public NigamJenningsModel()
    {
    }

    /// <summary>
    /// Run Nigam-Jenning's method
    /// </summary>
    /// <param name="h">Attenuation constant of structure</param>
    /// <param name="T">Natural period of the structure</param>
    public TimeHistory Calc(SDoFModel model, TimeHistory wave)
    {
        var resultHistory = (TimeHistory)wave.Clone();
        resultHistory.__resultFileName = "NigamJennings";

        var epModel = model.RFC;
        var h = model.h;
        var T = model.T;
        var TimeStep = wave.TimeStep;

        #region 一定となる係数の事前計算

        var wo = 2 * Math.PI / T;
        var wo2 = wo * wo;
        var wo3 = wo * wo * wo;
        var m = 0d;
        if (epModel != null)
            m = epModel.K1 / wo2;
        var e = Math.Pow(Math.E, -1.0 * h * wo * TimeStep);
        var h1 = Math.Sqrt(1.0 - h * h); // √1-h2
        var h2 = 2 * h * h - 1;          // 2h2-1
        var wd = h1 * wo;
        var sin = Math.Sin(wd * TimeStep);
        var cos = Math.Cos(wd * TimeStep);

        #endregion

        for (int i = 1; i < resultHistory.DataRowCount; i++)
        {
            var p = resultHistory.GetStep(i - 1);
            var c = resultHistory.GetStep(i);

            #region 係数の計算

            var a11 = e * (h / h1 * sin + cos);

            var a12 = e / wd * sin;

            var a21 = -e * wo / h1 * sin;

            var a22 = e * (cos - h / h1 * sin);

            var b11 =
                e
                *
                (
                    (h2 / wo2 / TimeStep + h / wo) * sin / wd
                    +
                    (2 * h / wo3 / TimeStep + 1 / wo2) * cos
                )
                -
                2 * h / wo3 / TimeStep
                ;

            var b12 =
                -e
                *
                (
                    h2 / wo2 / TimeStep * sin / wd
                    +
                    2 * h / wo3 / TimeStep * cos
                )
                -
                1 / wo2
                +
                2 * h / wo3 / TimeStep;


            var b21 =
                e
                *
                (
                    (h2 / wo2 / TimeStep + h / wo) * (cos - h / h1 * sin)
                    -
                    (2 * h / wo3 / TimeStep + 1 / wo2) * (wd * sin + h * wo * cos)
                )
                +

                    1 / wo2 / TimeStep
                ;

            var b22 =
               -e
               *
               (
                   h2 / wo2 / TimeStep * (cos - h / h1 * sin)
                   -
                   2 * h / wo3 / TimeStep * (wd * sin + h * wo * cos)
               )
               -
               1 / wo2 / TimeStep
               ;

            #endregion

            c.x = a11 * p.x + a12 * p.xt + b11 * p.ytt + b12 * p.ytt;
            c.xt = a21 * p.x + a22 * p.xt + b21 * p.ytt + b22 * p.ytt;

            if (epModel == null)
                c.xtt = p.ytt - 2 * h * wo * c.xt - wo2 * c.x;
            else
            {
                var F = epModel.CalcNextF(c.x);
                c.xtt = p.ytt - 2 * h * wo * c.xt - F / m;
                c.f = F;
            }

            resultHistory.SetStep(i, c);
        }

        return resultHistory;
    }

}
