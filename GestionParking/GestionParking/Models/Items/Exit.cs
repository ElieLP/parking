using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Exit
    {
        private int m_ID;
        private Coord m_location;
        private Boolean m_Opened;
        private Boolean m_sature;
        private int m_nbVoiture;
        private ExitState m_state;

        public Exit(int id, Coord coord)
        {
            this.m_ID = id;
            this.m_location = coord;
            this.m_nbVoiture = 0;
            this.m_state = ExitState.Opened;
        }

        public int ID
        {
            get
            {
                return m_ID;
            }

            set
            {
                m_ID = value;
            }
        }

        public Coord location
        {
            get
            {
               return m_location;
            }

            set
            {
                m_location = value;
            }
        }

        public Boolean Opened
        {
            get
            {
                return m_Opened;
            }

            set
            {
               m_Opened = value;
            }
        }

        public int nbVoiture
        {
            get
            {
                return m_nbVoiture;
            }

            set
            {
                m_nbVoiture = value;
            }
        }

        public ExitState State
        {
            get
            {
                return m_state;
            }

            set
            {
                m_state = value;
            }
        }

        public Boolean Sature
        {
            get
            {
                return m_sature;
            }

            set
            {
                m_sature = value;
            }
        }
    }
}