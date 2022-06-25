using TimeHistoryResponseAnalysis.Class.StructuralModel;
using Aki32_Utilities.OwesomeModels;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
public class NewmarkBetaModel : ITimeHistoryAnalysisModel
{

    // ★★★★★★★★★★★★★★★ props

    public double beta { get; set; }

    // ★★★★★★★★★★★★★★★ inits

    public NewmarkBetaModel(double beta)
    {
        this.beta = beta;
    }

    // ★★★★★★★★★★★★★★★ methods

    /// <summary>
    /// Run newmark beta method
    /// </summary>
    /// <param name="beta">Beta of Newmark Beta Method</param>
    /// <param name="h">Attenuation constant of structure</param>
    /// <param name="T">Natural period of the structure</param>
    public TimeHistory Calc(SDoFModel model, TimeHistory wave)
    {
        var resultHistory = (TimeHistory)wave.Clone();
        resultHistory.resultFileName = $"{wave.Name} - {model.RFC.GetType().Name} - {GetType().Name}";

        var rfcModel = model.RFC;
        var h = model.h;
        var m = model.m;
        var dt = wave.TimeStep;

        for (int i = 1; i < resultHistory.DataRowCount; i++)
        {
            var p = resultHistory.GetStep(i - 1);
            var c = resultHistory.GetStep(i);

            var F = rfcModel.CurrentF;
            var w = model.w;
            var w2 = w * w;

            // x → F/(m*w2)
            var xtt_nume1 = c.ytt + 2 * h * w * (p.xt + 0.5 * p.xtt * dt);
            var xtt_nume2 = w2 * ((F / (m * w2)) + p.xt * dt + (0.5 - beta) * p.xtt * dt * dt);
            var xtt_nume = xtt_nume1 + xtt_nume2;
            var xtt_denom = 1 + h * w * dt + beta * w2 * dt * dt;
            c.xtt = -xtt_nume / xtt_denom;
            c.xt = p.xt + 0.5 * (p.xtt + c.xtt) * dt;
            c.x = p.x + p.xt * dt + ((0.5 - beta) * p.xtt + beta * c.xtt) * dt * dt;
            c.xtt_ytt = c.xtt + c.ytt;
            c.f = rfcModel.CalcNextF(c.x);

            resultHistory.SetStep(i, c);
        }

        return resultHistory;
    }

    // ★★★★★★★★★★★★★★★

}
