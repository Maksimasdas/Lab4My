using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    internal class Exp
    {
        double x;
        double y;
        bool isflag = false;

        public Exp(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public bool Isflag { get => isflag; set => isflag = value; }
    }
}
