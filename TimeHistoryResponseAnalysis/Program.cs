using TimeHistoryResponseAnalysis.Class.ElastoPlasticModel;

namespace TimeHistoryResponseAnalysis
{
    internal class Program
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
                    //var inputCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = Wave.FromCsv(inputCsv);
                    //wave.CalcNewmarkBeta(0.25, 0.03, 1);
                    //wave.OutputWaveHistoryToCsv();
                }

                // nigam jennings
                {
                    //var inputCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = Wave.FromCsv(inputCsv);
                    //wave.CalcNigamJennings(0.03, 1);
                    //wave.OutputWaveHistoryToCsv();
                }

                // clough model
                {
                    var inputCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    var wave = Wave.FromCsv(inputCsv);
                    var Model = new CloughModel(2, 0.1, 10);
                    wave.CalcNigamJennings(0.03, 1, Model);
                    wave.OutputWaveHistoryToCsv();
                }

                // spectrum analysis with newmark beta
                // TODO : make this a class
                {
                    //var inputCsv = new FileInfo(@$"{basePath}\Hachinohe-NS.csv");
                    //var wave = Wave.FromCsv(inputCsv);

                    //var hList = new double[] { 0.00, 0.03, 0.05, 0.10 };
                    //foreach (var h in hList)
                    //{
                    //    var outputCsv = new FileInfo(Path.Combine(inputCsv.FullName, "output", $"spectrum(h={h}).csv"));
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
