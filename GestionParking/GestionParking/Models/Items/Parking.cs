using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Parking
    {
        private int m_carCount;
        private List<Floor> m_floorList;

        public int CarCount
        {
            get
            {
                return m_carCount;
            }

            set
            {
                m_carCount = value;
            }
        }

        private List<Floor> floorList
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                m_floorList = value;
            }
        }
    }
}