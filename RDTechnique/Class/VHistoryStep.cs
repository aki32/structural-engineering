namespace RDTechnique
{
    public class VHistoryStep
    {
        public double t { get; set; }
        public double v { get; set; }

        public const string ToStringHeader = "t,v";
        public string ToString() => $"{t},{v}";
    }
}
