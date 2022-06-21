


namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public interface IRestoringForceCharacteristics
{
    public double K1 { get; set; }
    public double CalcNextF(double targetX);
}
