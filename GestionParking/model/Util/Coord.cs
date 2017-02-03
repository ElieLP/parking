using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Coord
    {
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private int x
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        private int y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int Dist2D(Coord c)
        {
            return Math.Abs((this.x - c.x ) * (this.y - c.y));
        }
    }
}