namespace RainFlowCycleCounting
{
    public class MuHistoryStep
    {
        public double t { get; set; }
        public double mu { get; set; }

        public double totalMu { get; set; }
        public double totalDamage { get; set; }

        public const string ToStringHeader = "t,mu,totalMu,totalDamage";
        public string ToString() => $"{t},{mu},{totalMu},{totalDamage}";
    }
}
