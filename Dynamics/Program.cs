using Aki32_Utilities.OwesomeModels;
using Dynamics.Class.ElastoplasticAnalysis;
using Dynamics.Class.RainflowCycleCounting;
using Dynamics.Class.RDTechnique;

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

                    // ep test
                    {
                        //var epList = new List<ElastoplasticCharacteristic>
                        //{
                        //    new BilinearModel(2, 0.1, 80),
                        //    new CloughModel(2, 0.1, 80),
                        //    new DegradingCloughModel(2, 0.1, 80, 0.4),
                        //    new ElasticBilinearModel(2, 0.1, 80),
                        //    new ElasticTrilinearModel(2, 0.5, 80, 0.1, 100),
                        //    new ElasticTetralinearModel(2, 0.5, 80, 0.25, 90, 0.1, 110),
                        //};

                        //foreach (var ep in epList)
                        //{
                        //    var tester = new EPTester(ep);
                        //    var wave = EPTester.GetTestWave1();
                        //    var result = tester.Calc(wave);

                        //    var saveDir = new DirectoryInfo(@$"{basePath}\output");
                        //    result.SaveToCsv(saveDir);
                        //}
                    }

                    // combined
                    {
                        //var epList = new List<ElastoplasticCharacteristic>
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

                        //foreach (var ep in epList)
                        //{
                        //    foreach (var waveAnalysisModel in waveAnalysisModelList)
                        //    {
                        //        var model = SDoFModel.FromT(1, 0.03, ep);
                        //        var result = model.Calc(wave, waveAnalysisModel);
                        //        result.SaveToCsv();
                        //    }
                        //}
                    }

                    // spectrum analysis
                    {
                        //var ep = new ElasticModel(2);
                        ////var ep = new DegradingCloughModel(2, 0.1, 8, 0.4);

                        //var TList = Enumerable.Range(100, 400).Select(x => x / 100d).ToArray();
                        //var hList = new double[] { 0.00, 0.03, 0.05, 0.10 };

                        //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                        //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                        ////var waveAnalysisModel = new NewmarkBetaModel(0.25);
                        //var waveAnalysisModel = new NigamJenningsModel();

                        //var resultSet = SDoFModel.CalcResponseSpectrum(TList, hList, wave, waveAnalysisModel, ep);
                        //resultSet.SaveToExcel(waveCsv.Directory);
                    }

                }

                // RainflowCycleCounting
                {
                    //var basePath = @"..\..\..\# TestModel\RainflowCycleCounting";

                    //// Basic
                    //{
                    //    // Define IO paths
                    //    var inputCsvPath = Path.Combine(basePath, @"input1.csv");
                    //    var outputCsvPath = Path.Combine(basePath, @"result1.csv");

                    //    var muHistory = MuHistory.FromCsv(inputCsvPath);
                    //    muHistory.CalcRainflow(5, 1 / 3d, false);
                    //    muHistory.OutputDamageHistoryToCsv(outputCsvPath);
                    //}
                }

                // RDTechnique
                {
                    //var basePath = @"..\..\..\# TestModel\RDTechnique";

                    //// Define IO paths
                    //var inputCsvPath = Path.Combine(basePath, @"input.csv");
                    //var outputCsvPath = Path.Combine(basePath, @"result.csv");
                    
                    //// Read input csv
                    //var vHistory = VHistory.FromCsv(inputCsvPath);

                    //// RD Technique
                    //var RDResult = vHistory.CalcRD(200);
                    //RDResult.OutputResultToCsv(outputCsvPath);

                    //// Post process
                    //var att = RDResult.CalcAttenuationConstant(4, true);
                    //Console.WriteLine();
                    //Console.WriteLine($"result h = {att}");
                }

            }

            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
