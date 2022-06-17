


namespace TimeHistoryResponseAnalysis.Class.ElastoPlasticModel
{
    internal interface IElastoPlasticModel
    {
        public double K1 { get; set; }
        internal double CalcNextF(double targetX);
    }
}
