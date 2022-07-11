using Aki32_Utilities.Extensions;
using Dynamics.RainflowCycleCounting;
using System.Text;

namespace Dynamics.RainflowCycleCounting;
public static class RainflowCycleCountingExtension
{

    /// <summary>
    /// Rainflow
    /// </summary>
    /// <param name="inputFile"></param>
    /// <param name="outputFile">when null, automatically set to {inputFile.DirectoryName}/output_Rainflow/{inputFile.Name}</param>
    /// <returns></returns>
    public static FileInfo Rainflow(this FileInfo inputFile, FileInfo? outputFile, double C, double beta, bool consoleOutput = false, bool outputRainBranches = false, FileInfo? outputRainBranchesFile = null)
    {
        // preprocess
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // to handle Shift-JIS

        if (outputFile is null)
            outputFile = new FileInfo(Path.Combine(inputFile.DirectoryName, "output_Rainflow", inputFile.Name));
        if (!outputFile.Directory.Exists) outputFile.Directory.Create();
        if (outputFile.Exists) outputFile.Delete();

        if (outputRainBranches)
        {
            if (outputRainBranchesFile is null)
                outputRainBranchesFile = new FileInfo(Path.Combine(inputFile.DirectoryName, "output_Rainflow", $"{Path.GetFileNameWithoutExtension(inputFile.Name)}_Branches.csv"));
            if (!outputRainBranchesFile.Directory.Exists) outputRainBranchesFile.Directory.Create();
            if (outputRainBranchesFile.Exists) outputRainBranchesFile.Delete();
        }

        // main
        var rainflow = RainflowCalculator.FromCsv(inputFile);
        rainflow.CalcRainflow(C, beta, consoleOutput);
        rainflow.SaveResultHistoryToCsv(outputFile);

        if (outputRainBranches)
        {
            rainflow.SaveRainBranchesToCsv(outputRainBranchesFile);
        }

        return outputFile;
    }

    /// <summary>
    /// Rainflow
    /// </summary>
    /// <param name="inputDir"></param>
    /// <param name="outputDir">when null, automatically set to {inputDir.FullName}/output_Rainflow</param>
    /// <returns></returns>
    public static DirectoryInfo Rainflow_Loop(this DirectoryInfo inputDir, DirectoryInfo? outputDir, double C, double beta, bool outputRainBranches = false, DirectoryInfo? outputRainBranchesDir = null)
    {
        // preprocess
        if (UtilConfig.ConsoleOutput)
            Console.WriteLine("\r\n** Rainflow_Loop() Called");
        if (outputDir is null)
            outputDir = new DirectoryInfo(Path.Combine(inputDir.FullName, "output_Rainflow"));
        if (!outputDir.Exists) outputDir.Create();

        if (outputRainBranches)
        {
            if (outputRainBranchesDir is null)
                outputRainBranchesDir = new DirectoryInfo(Path.Combine(inputDir.FullName, "output_Rainflow"));
            if (!outputRainBranchesDir.Exists) outputRainBranchesDir.Create();
        }

        // main
        foreach (var file in inputDir.GetFiles())
        {
            var newFilePath = Path.Combine(outputDir.FullName, file.Name);
            try
            {
                if (outputRainBranches)
                {
                    file.Rainflow(new FileInfo(newFilePath), C, beta);
                }
                else
                {
                    var newRainBranchesFilePath = Path.Combine(outputRainBranchesDir!.FullName, $"{Path.GetFileNameWithoutExtension(file.Name)}_Branches.csv");
                    file.Rainflow(new FileInfo(newFilePath), C, beta,
                        outputRainBranches: outputRainBranches, outputRainBranchesFile: new FileInfo(newRainBranchesFilePath));
                }
                if (UtilConfig.ConsoleOutput)
                    Console.WriteLine($"O: {newFilePath}");
            }
            catch (Exception ex)
            {
                if (UtilConfig.ConsoleOutput)
                    Console.WriteLine($"X: {newFilePath}, {ex.Message}");
            }
        }

        return outputDir;
    }

}
