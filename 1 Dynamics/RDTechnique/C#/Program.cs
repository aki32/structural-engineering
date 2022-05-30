namespace RDTechnique
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();



            // Define IO paths
            var baseDirPath = @"..\..\..\..\# TestModel";
            var inputCsvPath = Path.Combine(baseDirPath, @"input.csv");
            var outputCsvPath = Path.Combine(baseDirPath, @"result.csv");


            // Read input csv
            var vHistory = VHistory.FromCsv(inputCsvPath);


            // RD Technique
            var RDResult = vHistory.CalcRD(200,10,0);
            RDResult.OutputResultToCsv(outputCsvPath);


            // Post process
            RDResult.CalcAttenuationConstant(4, true);


            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
