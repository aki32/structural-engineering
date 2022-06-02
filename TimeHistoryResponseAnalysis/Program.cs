namespace TimeHistoryResponseAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();



            // Define IO paths
            var baseDirPath = @"..\..\..\# TestModel";
            var inputDirPath = Path.Combine(baseDirPath, "input");
            var inputCsvPath = Path.Combine(inputDirPath, @"Hachinohe-NS.csv");
            var outputDirPath = Path.Combine(baseDirPath, "output");
            var outputCsvPath = Path.Combine(outputDirPath, @"result.csv");


            // Read input csv
            var wave = Wave.FromCsv(inputCsvPath);


            // Run Newmark Beta Method
            if (true)
            {
                wave.CalcNewmarkBeta(0.25, 0.03, 1);
                wave.OutputWaveHistoryToCsv(outputCsvPath);
            }


            // Run Spectrum Analysis with Newmark Beta Method
            if (true)
            {
                var hList = new double[] { 0.00, 0.03, 0.05, 0.10 };
                foreach (var h in hList)
                {
                    outputCsvPath = Path.Combine(outputDirPath, $"spectrum(h={h}).csv");
                    using var sw = new StreamWriter(outputCsvPath);
                    sw.WriteLine(SpectrumSet.ToStringHeader);

                    for (int i = 100; i <= 500; i++)
                    {
                        var T = i / 100d;
                        wave.CalcNewmarkBeta(0.25, h, T);
                        var ss = wave.GetSpectrumSet();
                        sw.WriteLine(ss.ToString());
                    }

                    Console.WriteLine($"Output Succeeded: h={h}");
                    Console.WriteLine($"--------------------------");
                }
            }



            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
