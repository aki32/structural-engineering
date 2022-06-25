using TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
using TimeHistoryResponseAnalysis.Class.TimeHistoryAnalysisModel;
using Aki32_Utilities.OwesomeModels;
using Aki32_Utilities.Extensions;

namespace TimeHistoryResponseAnalysis.Class.StructuralModel;
public static class SDoFSpectrumCalculator
{

    // ★★★★★★★★★★★★★★★

    //public static (TimeHistory Sd, TimeHistory Sv, TimeHistory Sa)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="TList"></param>
    /// <param name="hList"></param>
    /// <param name="wave"></param>
    /// <param name="thaModel"></param>
    /// <param name="rfc"></param>
    /// <returns>
    /// List<TimeHistory> {Sd, Sv, Sa};
    /// </returns>
    public static List<TimeHistory> CalcSpectrum(double[] TList, double[] hList, TimeHistory wave, ITimeHistoryAnalysisModel thaModel, RestoringForceCharacteristics.RestoringForceCharacteristics rfc = null)
    {
        if (rfc == null)
            rfc = new ElasticModel(1);

        Console.WriteLine("============================================");
        Console.WriteLine("calculating…");

        var SdList = new TimeHistory("1 Sd");
        var SvList = new TimeHistory("2 Sv");
        var SaList = new TimeHistory("3 Sa");

        foreach (var T in TList)
        {
            var u = TList.ToList().IndexOf(T) * hList.Length;
            var b = TList.Length * hList.Length;
            Console.Write($"{u} / {b} ( {100 * u / b} %)");

            var Sd = new TimeHistoryStep();
            var Sv = new TimeHistoryStep();
            var Sa = new TimeHistoryStep();
            Sd["T"] = T;
            Sv["T"] = T;
            Sa["T"] = T;

            foreach (var h in hList)
            {
                var targetStructure = SDoFModel.FromT(T, h, rfc);
                var resultSpectrum = targetStructure.Calc(wave, thaModel).GetSpectrumSet();
                Sd[$"h={h:F4}"] = resultSpectrum.Sd;
                Sv[$"h={h:F4}"] = resultSpectrum.Sv;
                Sa[$"h={h:F4}"] = resultSpectrum.Sa;
            }

            SdList.AppendStep(Sd);
            SvList.AppendStep(Sv);
            SaList.AppendStep(Sa);

            ConsoleExtension.ClearCurrentConsoleLine();
        }

        Console.WriteLine("calculation finished");
        Console.WriteLine("============================================");

        return new List<TimeHistory> { SdList, SvList, SaList };
    }


    // ★★★★★★★★★★★★★★★

}
