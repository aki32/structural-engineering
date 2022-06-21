namespace RainFlowCycleCounting
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();


            // Basic
            {
                //// Define IO paths
                ////var baseDirPath = @"..\..\..\# TestModel";
                ////var inputCsvPath = Path.Combine(baseDirPath, @"input1.csv");
                ////var outputCsvPath = Path.Combine(baseDirPath, @"result1.csv");


                //var baseDirPath = @"C:\Users\aki32\Dropbox\Documents\02 東大関連\0 授業\3 建築学専攻\建築構造・材料演習\# 演習\e-defenseモデル\calc";
                //var inputCsvPath = Path.Combine(baseDirPath, @"input1.csv");
                //var outputCsvPath = Path.Combine(baseDirPath, @"result1.csv");

                //var muHistory = MuHistory.FromCsv(inputCsvPath);
                //muHistory.CalcRainflow(5, 1 / 3d, false);
                //muHistory.OutputDamageHistoryToCsv(outputCsvPath);
            }

            // For all files in dir
            {
                var inputDirPath = @"C:\Users\aki32\Dropbox\Documents\02 東大関連\0 授業\3 建築学専攻\建築構造・材料演習\# 演習\e-defenseモデル\calc\集計処理, history, organized";
                var outputDirPath = @"C:\Users\aki32\Dropbox\Documents\02 東大関連\0 授業\3 建築学専攻\建築構造・材料演習\# 演習\e-defenseモデル\calc\集計処理, history, rainflow";

                foreach (var inputCsvPath in Directory.GetFiles(inputDirPath))
                {
                    var muHistory = MuHistory.FromCsv(inputCsvPath);
                    muHistory.CalcRainflow(5, 1 / 3d, false);
                    var outputCsvPath = Path.Combine(outputDirPath, Path.GetFileName(inputCsvPath));
                    muHistory.OutputDamageHistoryToCsv(outputCsvPath);
                    Console.WriteLine(inputCsvPath);
                }
            }


            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
