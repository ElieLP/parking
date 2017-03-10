using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Exit
    {
        public Exit(int id, Coord coord)
        {
            this.ID = id;
            this.location = coord;

        }

        public int ID
        {
            get
            {
                return ID;
            }

            set
            {
                ID = value;
            }
        }

        public Coord location
        {
            get
            {
               return location;
            }

            set
            {
                location = value;
            }
        }

        public Boolean Opened
        {
            get
            {
                return Opened;
            }

            set
            {
                Opened = value;
            }
        }
    }
}