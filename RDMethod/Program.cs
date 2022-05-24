namespace RDMethod
{
    internal class Program
    {
        /// <summary>
        /// RD法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();



            // Define IO paths
            var baseDirPath = @"C:\Users\aki32\Dropbox\Codes\# Projects\# Utilities\Structural Engineering\# Integrated\RDMethod\# TestModel";
            var inputCsvPath = Path.Combine(baseDirPath, @"input.csv");
            var outputCsvPath = Path.Combine(baseDirPath, @"result.csv");


            // Read input csv
            var vHistory = VHistory.FromCsv(inputCsvPath);

            
            // RD Method
            if (true)
            {
                var RDResult = vHistory.CalcRD(200);
                RDResult.OutputResultToCsv(outputCsvPath);
            }



            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
