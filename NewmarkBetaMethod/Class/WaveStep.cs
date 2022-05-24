using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewmarkBetaMethod
{
    internal class WaveStep
    {
        public double t { get; set; }
        public double ytt { get; set; }
        public double xtt { get; set; }
        public double xt { get; set; }
        public double x { get; set; }

        public const string ToStringHeader = "t,y'',x'',x',x";
        public string ToString() => $"{t},{ytt},{xtt},{xt},{x}";
    }
}
