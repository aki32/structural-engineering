using Aki32_Utilities.Extensions;
using Aki32_Utilities.OwesomeModels;
using Dynamics.ElastoplasticAnalysis;
using Dynamics.RainflowCycleCounting;
using Dynamics.RDTechnique;

namespace Dynamics
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();


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
                        //var waveAnalysisModelList = new List<ITimeHistoryAnalysisModel>
                        //    {
                        //        new NewmarkBetaModel(0.25),
                        //        //new NigamJenningsModel(),
                        //    };

                        //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                        //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                        //foreach (var waveAnalysisModel in waveAnalysisModelList)
                        //{
                        //    var epList = new List<ElastoplasticCharacteristic>
                        //    {
                        //        new ElasticModel(2),
                        //        new BilinearModel(2, 0.1, 8),
                        //        new CloughModel(2, 0.1, 8),
                        //        new DegradingCloughModel(2, 0.1, 8, 0.4),
                        //    };

                        //    foreach (var ep in epList)
                        //    {
                        //        try
                        //        {
                        //            var model = SDoFModel.FromT(1, 0.03, ep);
                        //            var result = model.Calc(wave, waveAnalysisModel);
                        //            result.SaveToCsv();
                        //            Console.WriteLine("[O]");
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            Console.WriteLine($"[X]: {ex.Message}");
                        //            throw;
                        //        }
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
                    ////var inputCsv = new FileInfo(Path.Combine(basePath, @"input3.csv"));
                    //var inputCsv = new FileInfo(@"C:\Users\aki32\Desktop\anaAll_beam.csv");

                    //var rainflow = RainflowCalculator.FromCsv(inputCsv);
                    //rainflow.CalcRainflow(5, 1 / 3d, false);
                    //rainflow.SaveResultHistoryToCsv();
                    //rainflow.SaveRainBranchesToCsv();

                    //// inputCsv.Rainflow(null, 5, 1 / 3d);
                }

                // RDTechnique
                {
                    //var basePath = @"..\..\..\# TestModel\RDTechnique";

                    //// Define IO paths
                    //var inputCsv = new FileInfo(Path.Combine(basePath, @"input.csv")).OsSafe();

                    //// Read input csv
                    //var rd = RDTechniqueCalculator.FromCsv(inputCsv);

                    //// RD Technique
                    //rd.CalcRD(200);
                    //rd.SaveResultHistoryToCsv();

                    //// Post process
                    //var att = rd.CalcAttenuationConstant(4, true);
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
