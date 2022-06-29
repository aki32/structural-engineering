

namespace Dynamics.Class.RainflowCycleCounting;
public class MuHistory
{
    public MuHistoryStep[] Steps { get; set; }


    /// <summary>
    /// Forbid public  instantiate
    /// </summary>
    private MuHistory() { }


    /// <summary>
    /// Construct from csv
    /// </summary>
    /// <param name="inputCsvPath"></param>
    /// <exception cref="Exception"></exception>
    public static MuHistory FromCsv(string inputCsvPath)
    {
        var muHistory = new MuHistory();

        try
        {
            var input = File.ReadLines(inputCsvPath, System.Text.Encoding.UTF8).ToArray();

            // Get Steps
            muHistory.Steps = input
                .Select(x => x.Split(new string[] { "," }, StringSplitOptions.None))
                .Skip(1)
                .Select(x => new MuHistoryStep() { t = double.Parse(x[0]), mu = double.Parse(x[1]) })
                .ToArray();

        }
        catch (Exception e)
        {
            throw new Exception("Failed to read input csv");
        }

        return muHistory;
    }

    /// <summary>
    /// Run rainflow cycle counting.
    /// </summary>
    /// <param name="C">Damage-related coefficient</param>
    /// <param name="beta">Damage-related coefficeint</param>
    public void CalcRainflow(double C, double beta, bool consoleOutput = false)
    {
        var AllRainBranches = new RainBranches[]
        {
            new RainBranches(true),
            new RainBranches(false),
        };

        for (int i = 0; i < Steps.Length; i++)
        {
            if (consoleOutput)
                Console.WriteLine($"step {i}");

            var currentStep = Steps[i];
            var lastStep = i > 0 ? Steps[i - 1] : new MuHistoryStep() { mu = 0 };

            currentStep.totalMu = 0;
            currentStep.totalDamage = 0;

            foreach (var RainBranch in AllRainBranches)
            {
                RainBranch.CalcNext(lastStep.mu, currentStep.mu, consoleOutput);
                currentStep.totalMu += RainBranch.TotalMu;
                currentStep.totalDamage += RainBranch.TotalDamage(C, beta);
            }
        }
    }

    /// <summary>
    /// Output damage history to csv
    /// </summary>
    /// <param name="outputFilePath"></param>
    public void OutputDamageHistoryToCsv(string outputCsvPath)
    {
        using var sw = new StreamWriter(outputCsvPath);
        sw.WriteLine(MuHistoryStep.ToStringHeader);
        foreach (var step in Steps)
            sw.WriteLine(step.ToString());
    }

}
