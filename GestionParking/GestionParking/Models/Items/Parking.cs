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
        private static double m_tarif = 1.10;
        private double m_sommeRecoltee = 0;
        private static TimeSpan m_ouverture = new TimeSpan(6, 30, 0);
        private static TimeSpan m_fermeture = new TimeSpan(22, 30, 0);
        

        public Parking()
        {
            this.m_carCount = 0;
        }

        public int CarCount
        {
            get
            {
                return m_carCount;
            }

            private set
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

        public double Tarif
        {
            get
            {
                return m_tarif;
            }
        }

        public double SommeCollectee
        {
            get
            {
                return m_sommeRecoltee;
            }

            set
            {
                m_sommeRecoltee = value;
            }
        }

        public TimeSpan Ouverture
        {
            get
            {
                return m_ouverture;
            }
        }

        public TimeSpan Fermeture
        {
            get
            {
                return m_fermeture;
            }
        }

        public void incvalue()
        {
            this.m_carCount += 1;
        }

        public void pay(TimeSpan p_time)
        {
            this.m_sommeRecoltee += ((p_time.Hours + 1) * m_tarif);
        }
    }
}