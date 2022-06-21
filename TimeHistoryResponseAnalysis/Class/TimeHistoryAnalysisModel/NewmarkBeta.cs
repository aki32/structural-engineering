using TimeHistoryResponseAnalysis.Class.ElastoPlasticModel;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel
{
    public class NewmarkBetaModel : ITimeHistoryAnalysisModel
    {
        public double beta { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="beta"></param>
        public NewmarkBetaModel(double beta)
        {
            this.beta = beta;
        }

        /// <summary>
        /// Run newmark beta method
        /// </summary>
        /// <param name="beta">Beta of Newmark Beta Method</param>
        /// <param name="h">Attenuation constant of structure</param>
        /// <param name="T">Natural period of the structure</param>
        public TimeHistory Calc(SDoFModel model, TimeHistory wave)
        {
            var resultHistory = (TimeHistory)wave.Clone();
            resultHistory.__resultFileName = "NewmarkBeta";

            var epModel = model.epmodel;
            var h = model.h;
            var T = model.T;
            var TimeStep = wave.TimeStep;

            var wo = 2 * Math.PI / T;
            var xtt_sub = 1 + h * wo * TimeStep + beta * wo * wo * TimeStep * TimeStep;

            for (int i = 1; i < resultHistory.DataRowCount; i++)
            {
                var p = resultHistory.GetStep(i - 1);
                var c = resultHistory.GetStep(i);
                c.xtt = -(c.ytt + 2 * h * wo * (p.xt + 0.5 * p.xtt * TimeStep) + wo * wo * (p.x + p.xt * TimeStep + (0.5 - beta) * p.xtt * TimeStep * TimeStep) / xtt_sub);
                c.xt = p.xt + 0.5 * (p.xtt + c.xtt) * TimeStep;
                c.x = p.x + p.xt * TimeStep + ((0.5 - beta) * p.xtt + beta * c.xtt) * TimeStep * TimeStep;

                resultHistory.SetStep(i, c);
            }

            return resultHistory;
        }

    }
}
