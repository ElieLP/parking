using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Floor
    {
        public Floor(int iD ,Slot[] sl)
        {
            this.ID = iD;
            this.slotList = sl;
        }

        public int ID
        {
            get
            {
                return ID;
            }
            private set
            {
                this.ID = value;
            }
        }

        private Slot[] slotList
        {
            get
            {
                return slotList;
            }

            set
            {
                slotList = value;
            }
        }

        public List<Slot> GetSlotsByState(SlotState s)
        {
            List<Slot> temp= new List<Slot>();
            foreach (Slot current in slotList)
                if (current.state == s)
                {
                    temp.Add(current);
                }
                return temp;
        }
    }
}