using Aki32_Utilities.OwesomeModels;
using Dynamics.Class.ElastoplasticAnalysis;
using Dynamics.Class.RainflowCycleCounting;

namespace Dynamics
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();

            // TODO:
            // merge rainflow
            // merge rd method

            // tests
            {
                // ElastoplasticAnalysis
                {
                    var basePath = @"..\..\..\# TestModel\ElastoplasticAnalysis";

                    // newmark beta
                    {
                        //var model = SDoFModel.FromT(1, 0.03);

                        //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                        //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                        //var waveAnalysisModel = new NewmarkBetaModel(0.25);
                        //var result = model.Calc(wave, waveAnalysisModel);

                        //result.SaveToCsv();
                    }

                    // nigam jennings
                    {
                        //var model = SDoFModel.FromT(1, 0.03);

                        //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                        //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                        //var waveAnalysisModel = new NigamJenningsModel();
                        //var result = model.Calc(wave, waveAnalysisModel);

                        //result.SaveToCsv();
                    }

                    // rfc test
                    {
                        //var rfcList = new List<RestoringForceCharacteristic>
                        //{
                        //    new BilinearModel(2, 0.1, 80),
                        //    new CloughModel(2, 0.1, 80),
                        //    new DegradingCloughModel(2, 0.1, 80, 0.4),
                        //    new ElasticBilinearModel(2, 0.1, 80),
                        //    new ElasticTrilinearModel(2, 0.5, 80, 0.1, 100),
                        //    new ElasticTetralinearModel(2, 0.5, 80, 0.25, 90, 0.1, 110),
                        //};

                        //foreach (var rfc in rfcList)
                        //{
                        //    var tester = new RFCTester(rfc);
                        //    var wave = RFCTester.GetTestWave1();
                        //    var result = tester.Calc(wave);

                        //    var saveDir = new DirectoryInfo(@$"{basePath}\output");
                        //    result.SaveToCsv(saveDir);
                        //}
                    }

                    // combined
                    {
                        //var rfcList = new List<RestoringForceCharacteristic>
                        //{
                        //    new BilinearModel(2, 0.1, 8),
                        //    new CloughModel(2, 0.1, 8),
                        //    new DegradingCloughModel(2, 0.1, 8, 0.4),
                        //};

                        //var waveAnalysisModelList = new List<ITimeHistoryAnalysisModel>
                        //{
                        //    new NewmarkBetaModel(0.25),
                        //    new NigamJenningsModel(),
                        //};

                        //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                        //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                        //foreach (var rfc in rfcList)
                        //{
                        //    foreach (var waveAnalysisModel in waveAnalysisModelList)
                        //    {
                        //        var model = SDoFModel.FromT(1, 0.03, rfc);
                        //        var result = model.Calc(wave, waveAnalysisModel);
                        //        result.SaveToCsv();
                        //    }
                        //}
                    }

                    // spectrum analysis
                    {
                        //var rfc = new ElasticModel(2);
                        ////var rfc = new DegradingCloughModel(2, 0.1, 8, 0.4);

                        //var TList = Enumerable.Range(100, 400).Select(x => x / 100d).ToArray();
                        //var hList = new double[] { 0.00, 0.03, 0.05, 0.10 };

                        //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                        //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                        ////var waveAnalysisModel = new NewmarkBetaModel(0.25);
                        //var waveAnalysisModel = new NigamJenningsModel();

                        //var resultSet = SDoFModel.CalcResponseSpectrum(TList, hList, wave, waveAnalysisModel, rfc);
                        //resultSet.SaveToExcel(waveCsv.Directory);
                    }

                }

                // RainflowCycleCounting
                {
                    var basePath = @"..\..\..\# TestModel\RainflowCycleCounting";

                    // Basic
                    {
                        // Define IO paths
                        var baseDirPath = @"..\..\..\# TestModel";
                        var inputCsvPath = Path.Combine(baseDirPath, @"input1.csv");
                        var outputCsvPath = Path.Combine(baseDirPath, @"result1.csv");

                        var muHistory = MuHistory.FromCsv(inputCsvPath);
                        muHistory.CalcRainflow(5, 1 / 3d, false);
                        muHistory.OutputDamageHistoryToCsv(outputCsvPath);
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
