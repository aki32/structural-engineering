using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeHistoryResponseAnalysis.Class.ElastoPlasticModel;

namespace TimeHistoryResponseAnalysis
{
    internal class Wave
    {
        public double TimeStep { get; set; }
        public WaveStep[] Steps { get; set; }

        #region フィールドたち
        private double __T;
        private double __h;
        private string __resultFileName = "result";
        private DirectoryInfo __inputDir;
        #endregion

        /// <summary>
        /// Forbid public instantiate
        /// </summary>
        private Wave() { }

        /// <summary>
        /// Construct from csv
        /// </summary>
        /// <param name="inputCsvPath"></param>
        /// <exception cref="Exception"></exception>
        public static Wave FromCsv(FileInfo inputCsv)
        {
            var wave = new Wave() { __inputDir = inputCsv.Directory };

            try
            {
                var input = File.ReadLines(inputCsv.FullName, Encoding.UTF8).ToArray();

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
        /// Run newmark beta method
        /// </summary>
        /// <param name="beta">Beta of Newmark Beta Method</param>
        /// <param name="h">Attenuation constant of structure</param>
        /// <param name="T">Natural period of the structure</param>
        public void CalcNewmarkBeta(double beta, double h, double T)
        {
            __T = T;
            __h = h;
            __resultFileName = "NewmarkBeta";

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
        /// Run Nigam-Jenning's method
        /// </summary>
        /// <param name="h">Attenuation constant of structure</param>
        /// <param name="T">Natural period of the structure</param>
        public void CalcNigamJennings(double h, double T, IElastoPlasticModel? epModel = null)
        {
            __T = T;
            __h = h;
            __resultFileName = "NigamJennings";

            #region 一定となる係数の事前計算


            var wo = 2 * Math.PI / T;
            var wo2 = wo * wo;
            var wo3 = wo * wo * wo;
            var m = 0d;
            if (epModel != null)
                m = epModel.K1 / wo2;
            var e = Math.Pow(Math.E, (-1.0 * h * wo * TimeStep));
            var h1 = Math.Sqrt(1.0 - h * h); // √1-h2
            var h2 = 2 * h * h - 1;          // 2h2-1
            var wd = h1 * wo;
            var sin = Math.Sin(wd * TimeStep);
            var cos = Math.Cos(wd * TimeStep);

            #endregion

            for (int i = 1; i < Steps.Length; i++)
            {
                var p = Steps[i - 1];
                var c = Steps[i];

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
                    (
                        2 * h / wo3 / TimeStep
                    );

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
                    (
                        1 / wo2 / TimeStep
                    );

                var b22 =
                   -e
                   *
                   (
                       (h2 / wo2 / TimeStep) * (cos - h / h1 * sin)
                       -
                       (2 * h / wo3 / TimeStep) * (wd * sin + h * wo * cos)
                   )
                   -
                   (
                       1 / wo2 / TimeStep
                   );

                #endregion

                c.x = a11 * p.x + a12 * p.xt + b11 * p.ytt + b12 * p.ytt;
                c.xt = a21 * p.x + a22 * p.xt + b21 * p.ytt + b22 * p.ytt;

                if (epModel == null)
                    c.xtt = p.ytt - 2 * h * wo * c.xt - wo2 * c.x;
                else
                    c.xtt = p.ytt - 2 * h * wo * c.xt - epModel.CalcNextF(c.x) / m;

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
        public void OutputWaveHistoryToCsv(FileInfo outputFile = null)
        {
            if (outputFile == null)
                outputFile = new FileInfo(Path.Combine(__inputDir.FullName, "output", $"result - {__resultFileName}.csv"));
            if (!outputFile.Directory.Exists)
                outputFile.Directory.Create();

            using var sw = new StreamWriter(outputFile.FullName);
            sw.WriteLine(WaveStep.ToStringHeader);
            foreach (var step in Steps)
                sw.WriteLine(step.ToString());
        }

    }
}
