using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewmarkBetaMethod
{
    internal class SpectrumSet
    {
        public double T { get; set; }
        public double Sx { get; set; }
        public double Sv { get; set; }
        public double Sa { get; set; }

        public SpectrumSet(double t, double sx, double sv, double sa)
        {
            T = t;
            Sx = sx;
            Sv = sv;
            Sa = sa;
        }

        public const string ToStringHeader = "T,Sx,Sv,Sa";
        public string ToString() => $"{T},{Sx},{Sv},{Sa}";
    }
}
