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
    public static FileInfo Rainflow(this FileInfo inputFile, FileInfo? outputFile, double C, double beta, bool consoleOutput = false)
    {
        // preprocess
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // to handle Shift-JIS
        if (outputFile is null)
            outputFile = new FileInfo(Path.Combine(inputFile.DirectoryName, "output_Rainflow", inputFile.Name));
        if (!outputFile.Directory.Exists) outputFile.Directory.Create();
        if (outputFile.Exists) outputFile.Delete();

        // main
        var muHistory = RainflowCalculator.FromCsv(inputFile);
        muHistory.CalcRainflow(C, beta, consoleOutput);
        muHistory.resultHistory.SaveToCsv(outputFile);

        return outputFile;
    }

    /// <summary>
    /// Rainflow
    /// </summary>
    /// <param name="inputDir"></param>
    /// <param name="outputDir">when null, automatically set to {inputDir.FullName}/output_Rainflow</param>
    /// <returns></returns>
    public static DirectoryInfo Rainflow_Loop(this DirectoryInfo inputDir, DirectoryInfo? outputDir, double C, double beta)
    {
        // preprocess
        if (UtilConfig.ConsoleOutput)
            Console.WriteLine("\r\n** Rainflow_Loop() Called");
        if (outputDir is null)
            outputDir = new DirectoryInfo(Path.Combine(inputDir.FullName, "output_Rainflow"));
        if (!outputDir.Exists) outputDir.Create();


        // main
        foreach (var file in inputDir.GetFiles())
        {
            var newFilePath = Path.Combine(outputDir.FullName, file.Name);
            try
            {
                file.Rainflow(new FileInfo(newFilePath), C, beta);
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
