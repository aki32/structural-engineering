namespace NewmarkBetaMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();



            // Define IO paths
            var baseDirPath = @"C:\Users\aki32\Dropbox\Codes\# Projects\# Utilities\Structural Engineering\# Integrated\NewmarkBetaMethod\# TestModel";
            var inputCsvPath = Path.Combine(baseDirPath, @"Hachinohe-NS.csv");
            var outputCsvPath = Path.Combine(baseDirPath, @"result.csv");


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
                foreach (var h in new double[] { 0.00, 0.03, 0.05, 0.10 })
                {
                    outputCsvPath = Path.Combine(baseDirPath, $"spectrum(h={h}).csv");
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
