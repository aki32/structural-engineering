using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewmarkBetaMethod
{
    internal class Wave
    {
        public double TimeStep { get; set; }
        public WaveStep[] Steps { get; set; }

        private double __T;
        private double __h;
        private double __beta;


        /// <summary>
        /// Forbidden public instanciate
        /// </summary>
        private Wave()
        {
           
        }


        /// <summary>
        /// Construct from csv
        /// </summary>
        /// <param name="inputCsvPath"></param>
        /// <exception cref="Exception"></exception>
        public static Wave FromCsv(string inputCsvPath)
        {
            var wave = new Wave();

            try
            {
                var input = File.ReadLines(inputCsvPath, Encoding.UTF8).ToArray();

                // Get TimeStep
                var temp = input
                    .Skip(1)
                    .Take(2)
                    .Select(x => x.Split(new string[] { "," }, StringSplitOptions.None)[0])
                    .Select(x => float.Parse(x))
                    .ToArray();

                wave.TimeStep = temp[1] - temp[0];

                // Get Steps
                wave.Steps = input
                    .Select(x => x.Split(new string[] { "," }, StringSplitOptions.None))
                    .Skip(1)
                    .Select(x => new WaveStep() { t = double.Parse(x[0]), ytt = double.Parse(x[1]) })
                    .ToArray();

            }
            catch (Exception e)
            {
                throw new Exception("Failed to read input csv");
            }

            return wave;
        }

        /// <summary>
        /// Run rainflow cycle counting.
        /// </summary>
        /// <param name="beta">Beta of Newmark Beta Method</param>
        /// <param name="h">Attenuation constant of structure</param>
        /// <param name="T">Natural period of the structure</param>
        public void CalcNewmarkBeta(double beta, double h, double T)
        {
            __beta = beta;
            __T = T;
            __h = h;

            var wo = 2 * Math.PI / T;
            var xtt_sub = 1 + h * wo * TimeStep + beta * wo * wo * TimeStep * TimeStep;

            for (int i = 1; i < Steps.Length; i++)
            {
                var p = Steps[i - 1];
                var c = Steps[i];
                c.xtt = -(c.ytt + 2 * h * wo * (p.xt + 0.5 * p.xtt * TimeStep) + wo * wo * (p.x + p.xt * TimeStep + (0.5 - beta) * p.xtt * TimeStep * TimeStep) / xtt_sub);
                c.xt = p.xt + 0.5 * (p.xtt + c.xtt) * TimeStep;
                c.x = p.x + p.xt * TimeStep + ((0.5 - beta) * p.xtt + beta * c.xtt) * TimeStep * TimeStep;
            }
        }

        /// <summary>
        /// Get response spectrum set from
        /// </summary>
        /// <returns></returns>
        public SpectrumSet GetSpectrumSet()
        {
            var Sx = Steps.Max(x => Math.Abs(x.x));
            var Sv = Steps.Max(x => Math.Abs(x.xt));
            var Sa = Steps.Max(x => Math.Abs(x.xtt));
            return new SpectrumSet(__T, Sx, Sv, Sa);
        }

        /// <summary>
        /// Output wave history to csv
        /// </summary>
        /// <param name="outputFilePath"></param>
        public void OutputWaveHistoryToCsv(string outputFilePath)
        {
            using var sw = new StreamWriter(outputFilePath);
            sw.WriteLine(WaveStep.ToStringHeader);
            foreach (var step in Steps)
                sw.WriteLine(step.ToString());
        }

    }
}
