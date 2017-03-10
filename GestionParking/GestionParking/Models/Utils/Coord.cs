using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Coord
    {
        private int m_x;
        private int m_y;

        public Coord(int p_x, int p_y)
        {
            this.m_x = p_x;
            this.m_y = p_y;
        }

        public int x
        {
            get
            {
                return m_x;
            }

            private set
            {
                m_x = value;
            }
        }

        public int y
        {
            get
            {
                return m_y;
            }

            private set
            {
                m_y = value;
            }
        }

        public int Dist2D(Coord c)
        {
            return Math.Abs((this.x - c.x ) * (this.y - c.y));
        }
    }
}