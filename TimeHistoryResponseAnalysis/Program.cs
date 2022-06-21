using TimeHistoryResponseAnalysis.Class.ElastoPlasticModel;
using TimeHistoryResponseAnalysis.Class.StructuralModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
using TimeHistoryResponseAnalysis.Class.TimeHistoryModel;

namespace TimeHistoryResponseAnalysis
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();

            // Test
            {
                var basePath = @"..\..\..\# TestModel";

                // newmark beta
                {
                    //var model = new SDoFModel(1, 0.03);

                    //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                    //var waveAnaModel = new NewmarkBetaModel(0.25);
                    //var result = model.Calc(wave, waveAnaModel);

                    //result.OutputWaveHistoryToCsv();
                }

                // nigam jennings
                {
                    //var model = new SDoFModel(1, 0.03);

                    //var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                    //var waveAnaModel = new NigamJenningsModel();
                    //var result = model.Calc(wave, waveAnaModel);

                    //result.OutputWaveHistoryToCsv();
                }

                // clough model + nigam jenning
                {
                    var epModel = new CloughModel(2, 0.1, 8);

                    var model = new SDoFModel(1, 0.03, epModel);

                    var waveCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    var wave = TimeHistory.FromCsv(waveCsv, new string[] { "t", "ytt" });

                    var waveAnaModel = new NigamJenningsModel();

                    var result = model.Calc(wave, waveAnaModel);

                    result.OutputWaveHistoryToCsv();
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
