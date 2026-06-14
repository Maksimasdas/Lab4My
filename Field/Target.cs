using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class Target
    {
        double x;
        double y;
        double rX;
        double lX;
        double uY;
        bool isExp = false;

        public Target()
        {

        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double RX { get => rX; set => rX = value; }
        public double LX { get => lX; set => lX = value; }
        public double UY { get => uY; set => uY = value; }
        public bool IsExp { get => isExp; set => isExp = value; }


    }
}
