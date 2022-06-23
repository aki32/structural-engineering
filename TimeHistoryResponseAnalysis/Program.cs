using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
using Aki32_Utilities.OwesomeModels;

namespace TimeHistoryResponseAnalysis
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();


            // TODO:
            // add characteristics

            // merge rainflow
            // merge rd method


            // Test
            {
                var basePath = @"..\..\..\# TestModel";

                // newmark beta
                {
                    //var model = SDoFModel.FromT(1, 0.03);

                    //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                    //var waveAnalysisModel = new NewmarkBetaModel(0.25);
                    //var result = model.Calc(wave, waveAnalysisModel);

                    //result.OutputTimeHistoryToCsv();
                }

                // nigam jennings
                {
                    //var model = SDoFModel.FromT(1, 0.03);

                    //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                    //var waveAnalysisModel = new NigamJenningsModel();
                    //var result = model.Calc(wave, waveAnalysisModel);

                    //result.OutputTimeHistoryToCsv();
                }

                // rfc test
                {
                    ////var rfc = new BilinearModel(2, 0.1, 80);
                    ////var rfc = new CloughModel(2, 0.1, 80);
                    //var rfc = new DegradingCloughModel(2, 0.1, 80, 0.4);

                    //var tester = new RFCTester(rfc);
                    //var wave = RFCTester.GetTestWave1();
                    //var result = tester.Calc(wave);

                    //var saveDir = new DirectoryInfo(@$"{basePath}\output");
                    //result.OutputTimeHistoryToCsv(saveDir);
                }

                // combined
                {
                    //var rfc = new ElasticModel(2);
                    //var rfc = new PerfectElastoPlasticModel(2, 8);
                    //var rfc = new BilinearModel(2, 0.1, 8);
                    //var rfc = new DegradingBilinearModel(2, 0.1, 8, 0.4);
                    //var rfc = new CloughModel(2, 0.1, 8);
                    var rfc = new DegradingCloughModel(2, 0.1, 8, 0.4);

                    var model = SDoFModel.FromT(1, 0.03, rfc);

                    var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                    //var waveAnalysisModel = new NewmarkBetaModel(0.25);
                    var waveAnalysisModel = new NigamJenningsModel();

                    var result = model.Calc(wave, waveAnalysisModel);

                    result.OutputTimeHistoryToCsv();
                }


                // spectrum analysis with newmark beta
                // TODO : make this a class
                {
                    //var inputCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = Wave.FromCsv(inputCsv);

                    //var hList = new double[] { 0.00, 0.03, 0.05, 0.10 };
                    //foreach (var h in hList)
                    //{
                    //    var outputCsv = new FileInfo(Path.Combine(inputCsv.DirectoryName, "output", $"spectrum(h={h}).csv"));
                    //    if (!outputCsv.Directory.Exists)
                    //        outputCsv.Directory.Create();
                    //    using var sw = new StreamWriter(outputCsv.FullName);
                    //    sw.WriteLine(SpectrumSet.ToStringHeader);

                    //    for (int i = 100; i <= 500; i++)
                    //    {
                    //        var T = i / 100d;
                    //        wave.CalcNewmarkBeta(0.25, h, T);
                    //        var ss = wave.GetSpectrumSet();
                    //        sw.WriteLine(ss.ToString());
                    //    }

                    //    Console.WriteLine($"Output Succeeded: h={h}");
                    //    Console.WriteLine($"--------------------------");
                    //}

                }

            }

            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
