namespace RainFlowCycleCounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();



            // Define IO paths
            var baseDirPath = @"C:\Users\aki32\Dropbox\Codes\# Projects\# Utilities\Structural Engineering\# Integrated\RainFlowCycleCounting\# TestModel";
            var inputCsvPath = Path.Combine(baseDirPath, @"input1.csv");
            var outputCsvPath = Path.Combine(baseDirPath, @"result1.csv");


            // Read input csv
            var muHistory = MuHistory.FromCsv(inputCsvPath);


            // Run rainflow cycle counting
            muHistory.CalcRainflow(5, 1 / 3d, true);
            

            // Output result csv
            muHistory.OutputDamageHistoryToCsv(outputCsvPath);



            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
