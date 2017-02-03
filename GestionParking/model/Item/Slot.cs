using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Slot
    {

        public Slot(int id, Coord coord)
        {
            this.ID = id;
            this.location = coord;
            this.lastTimeReserved = new DateTime(0);
            this.lastTimeTaken = new DateTime(0);
            this.state = SlotState.Empty;
        }

        public int ID
        {
            get
            {
                return ID;
            }

            private set
            {
                ID = value;
            }
        }

        public SlotState state
        {
            get
            {
                return state;
            }
            private set
            {
                state = value;
            }
        }

        public DateTime lastTimeTaken
        {
            get
            {
                return lastTimeTaken;
            }

            private set
            {
                lastTimeTaken = value;
            }
        }

        public DateTime lastTimeReserved
        {
            get
            {
                return lastTimeReserved;
            }

            private set
            {
                lastTimeReserved = value;
            }
        }

        public Coord location
        {
            get
            {
                return location;
            }

            private set
            {
                location = value;
            }
        }

        public void NewState(SlotState s)
        {
            switch (s)
            {
                case SlotState.Empty:
                    if (this.state == SlotState.Empty)
                        throw new Exception("Allready empty") ;
                        ;
                    break;
                case SlotState.Reserved:
                    if (this.state == SlotState.Reserved)
                        throw new Exception("Allready Reserved");
                    ;
                    lastTimeTaken = DateTime.Now;
                    break;
                case SlotState.Used:
                    if (this.state == SlotState.Used)
                        throw new Exception("Allready Used");
                    ;
                    lastTimeTaken = DateTime.Now;
                    break;
            }

            this.state = s;
        }
    }
}