

namespace Dynamics.Class.RDTechnique;
public class VHistory
{
    public double TimeStep { get; set; }
    public VHistoryStep[] Steps { get; set; }


    /// <summary>
    /// Forbid public  instantiate
    /// </summary>
    private VHistory() { }

    /// <summary>
    /// Construct from csv
    /// </summary>
    /// <param name="inputCsvPath"></param>
    /// <exception cref="Exception"></exception>
    public static VHistory FromCsv(string inputCsvPath)
    {
        var vHistory = new VHistory();

        try
        {
            var input = File.ReadLines(inputCsvPath, System.Text.Encoding.UTF8).ToArray();

            // Get TimeStep
            var temp = input
                .Skip(1)
                .Take(2)
                .Select(x => x.Split(new string[] { "," }, StringSplitOptions.None)[0])
                .Select(x => float.Parse(x))
                .ToArray();

            vHistory.TimeStep = temp[1] - temp[0];

            // Get Steps
            vHistory.Steps = input
                .Select(x => x.Split(new string[] { "," }, StringSplitOptions.None))
                .Skip(1)
                .Select(x => new VHistoryStep() { t = double.Parse(x[0]), v = double.Parse(x[1]) })
                .ToArray();

        }
        catch (Exception e)
        {
            throw new Exception("Failed to read input csv");
        }

        return vHistory;

    }


    /// <summary>
    /// Calculate RD Technique
    /// </summary>
    /// <returns></returns>
    public RDResultHistory CalcRD(int resultStepLength, int maxOverlayCount = int.MaxValue, int skipingInitialPeakCount = 0)
    {
        var resultHistory = new RDResultHistory(resultStepLength, TimeStep);
        int peakCount = 0;
        int overlayCount = 0;

        // ごり押し。極大を認識したら結果に足し合わせる。
        for (int i = 1; i < Steps.Length - resultStepLength - 1; i++)
        {
            if (maxOverlayCount <= overlayCount)
            {
                Console.WriteLine($"Successfully stopped: overlayCount count reached maxOverlayCount");
                break;
            }

            var lastStep = Steps[i - 1];
            var currentStep = Steps[i];
            var nextStep = Steps[i + 1];

            // Simple peak detection
            if (lastStep.v < currentStep.v && currentStep.v >= nextStep.v)
            {
                peakCount++;

                // Skip when in skipping state 
                if (skipingInitialPeakCount >= peakCount)
                    continue;

                // Overlay
                overlayCount++;
                for (int j = 0; j < resultStepLength; j++)
                    resultHistory.Steps[j].v += Steps[i + j].v;
            }
        }

        #region Show results

        if (skipingInitialPeakCount > 0)
            Console.WriteLine($"Skipped  : {peakCount - overlayCount} / {skipingInitialPeakCount}");

        if (maxOverlayCount == int.MaxValue)
            Console.WriteLine($"Overlayed: {overlayCount}");
        else
            Console.WriteLine($"Overlayed: {overlayCount} / {maxOverlayCount}");

        Console.WriteLine();

        #endregion

        return resultHistory;
    }
}
