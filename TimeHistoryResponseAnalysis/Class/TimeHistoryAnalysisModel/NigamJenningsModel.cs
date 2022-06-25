using Aki32_Utilities.OwesomeModels;
using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.StructuralModel;

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
        resultHistory.resultFileName = $"{wave.Name} - {model.RFC.GetType().Name} - {GetType().Name}";

        var rfcModel = model.RFC;
        var h = model.h;
        var m = model.m;
        var TimeStep = wave.TimeStep;

        var h1 = Math.Sqrt(1.0 - h * h); // √1-h2
        var h2 = 2 * h * h - 1;          // 2h2-1

        for (int i = 1; i < resultHistory.DataRowCount; i++)
        {
            var p = resultHistory.GetStep(i - 1);
            var c = resultHistory.GetStep(i);

            #region 係数の計算

            // 便利
            var w = model.w;
            var w2 = w * w;
            var w3 = w * w * w;
            var wd = h1 * w;
            var e = Math.Pow(Math.E, -1.0 * h * w * TimeStep);
            var sin = Math.Sin(wd * TimeStep);
            var cos = Math.Cos(wd * TimeStep);

            // nigam の係数
            var a11 = e * (h / h1 * sin + cos);

            var a12 = e / wd * sin;

            var a21 = -e * w / h1 * sin;

            var a22 = e * (cos - h / h1 * sin);

            var b11 =
                e
                *
                (
                    (h2 / w2 / TimeStep + h / w) * sin / wd
                    +
                    (2 * h / w3 / TimeStep + 1 / w2) * cos
                )
                -
                2 * h / w3 / TimeStep
                ;

            var b12 =
                -e
                *
                (
                    h2 / w2 / TimeStep * sin / wd
                    +
                    2 * h / w3 / TimeStep * cos
                )
                -
                1 / w2
                +
                2 * h / w3 / TimeStep;


            var b21 =
                e
                *
                (
                    (h2 / w2 / TimeStep + h / w) * (cos - h / h1 * sin)
                    -
                    (2 * h / w3 / TimeStep + 1 / w2) * (wd * sin + h * w * cos)
                )
                +

                    1 / w2 / TimeStep
                ;

            var b22 =
               -e
               *
               (
                   h2 / w2 / TimeStep * (cos - h / h1 * sin)
                   -
                   2 * h / w3 / TimeStep * (wd * sin + h * w * cos)
               )
               -
               1 / w2 / TimeStep
               ;

            #endregion

            c.x = a11 * p.x + a12 * p.xt + b11 * p.ytt + b12 * p.ytt;
            c.xt = a21 * p.x + a22 * p.xt + b21 * p.ytt + b22 * p.ytt;
            var F = rfcModel.CalcNextF(c.x);
            c.xtt = p.ytt - 2 * h * w * c.xt - F / m;  // wo2*x → F/m
            c.xtt_ytt = c.xtt + c.ytt;
            c.f = F;

            resultHistory.SetStep(i, c);
        }

        return resultHistory;
    }

}
