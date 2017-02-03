using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Parking
    {
        private int carCount;

        public int CarCount
        {
            get
            {
                return carCount;
            }

            set
            {
                carCount = value;
            }
        }

        private Floor[] floorList
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                floorList = value;
            }
        }
    }
}