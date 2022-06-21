


namespace TimeHistoryResponseAnalysis.Class.ElastoPlasticModel
{
    public interface IElastoPlasticModel
    {
        public double K1 { get; set; }
        public double CalcNextF(double targetX);
    }
}
