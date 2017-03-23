using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parking;

namespace Parking
{
    public delegate void ModelHandler<Floor>(Floor sender, FloorEventArgs e);

    public class FloorEventArgs : EventArgs
    {
        public FloorEventArgs()
        {

        }
    }

    public class Floor
    {
        private int m_ID;
        private List<Slot> m_slotList;
        private List<Exit> m_exitList;
        private Int32 m_carCount;


        public Floor(int p_iD ,List<Slot> p_sl,List<Exit> p_ex)
        {
            this.m_ID = p_iD;
            this.m_slotList = p_sl;
            this.m_exitList = p_ex;
            this.m_carCount = 0;
        }
        

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public List<Slot> slotList
        {
            get { return m_slotList; }
            set { m_slotList = value; }
        }

        public List<Exit> exitList
        {
            get { return m_exitList; }
            set { m_exitList = value; }
        }

        public Int32 carCount
        {
            get { return m_carCount; }
            set { m_carCount = value; }
        }

        public List<Slot> GetSlotsByState(SlotState s)
        {
            List<Slot> temp= new List<Slot>();
            foreach (Slot current in m_slotList)
                if (current.state == s)
                {
                    temp.Add(current);
                }
                return temp;
        }

        public void incvalue()
        {
            this.m_carCount += 1;
        }

        public void generateFloor1()
        {
            int i;
            int j;
            int lenghtPict = 72;
            int widthPict = 42;

            for (i = 0; i < 11; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (i != 2 && i != 5 && i != 8 && j != 4)
                    {
                        Coord tempCoord = new Coord(i * (lenghtPict + 10) + 40, j * (widthPict + 10) + 60);
                        Slot tempSlot = new Slot(m_slotList.Count, tempCoord);
                        m_slotList.Add(tempSlot);
                    }
                }
            }


            //Sorties
            Coord tempExitCoord = new Coord(0, (8 * (widthPict + 10) + 60) / 2 - 30 + 25);
            Exit exit1 = new Exit(0, tempExitCoord);
            tempExitCoord = new Coord(0, (8 * (widthPict + 10) + 60) / 2 + 30 + 25);
            Exit exit2 = new Exit(1, tempExitCoord);
            tempExitCoord = new Coord(11 * (lenghtPict + 10) + 62, (8 * (widthPict + 10) + 60) / 2 + 30 + 25);
            Exit exit3 = new Exit(2, tempExitCoord);
            tempExitCoord = new Coord(11 * (lenghtPict + 10) + 62, (8 * (widthPict + 10) + 60) / 2 - 30 + 25);
            Exit exit4 = new Exit(3, tempExitCoord);
            m_exitList.Add(exit1);
            m_exitList.Add(exit2);
            m_exitList.Add(exit3);
            m_exitList.Add(exit4);

        }

        public void generateFloor2()
        {
            int i;
            int j;
            int lenghtPict = 72;
            int widthPict = 42;

            for (i = 0; i < 11; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (i != 5 && j != 0 && j != 3 && j != 6)
                    {
                        Coord tempCoord = new Coord(i * (lenghtPict + 10) + 40, j * (widthPict + 10) + 40);
                        Slot tempSlot = new Slot(m_slotList.Count, tempCoord);
                        m_slotList.Add(tempSlot);
                    }
                }
            }

            //Sorties
            Coord tempExitCoord = new Coord(0, (8 * (widthPict + 10) + 60) / 2 - 80 + 35);
            Exit exit1 = new Exit(0, tempExitCoord);
            tempExitCoord = new Coord(0, (8 * (widthPict + 10) + 60) / 2 + 80 + 35);
            Exit exit2 = new Exit(1, tempExitCoord);
            tempExitCoord = new Coord(11 * (lenghtPict + 10) + 62, (8 * (widthPict + 10) + 60) / 2 + 80 + 35);
            Exit exit3 = new Exit(2, tempExitCoord);
            tempExitCoord = new Coord(11 * (lenghtPict + 10) + 62, (8 * (widthPict + 10) + 60) / 2 - 80 + 35);
            Exit exit4 = new Exit(3, tempExitCoord);
            m_exitList.Add(exit1);
            m_exitList.Add(exit2);
            m_exitList.Add(exit3);
            m_exitList.Add(exit4);
        }

        public void generateFloor3()
        {
            int i;
            int j;
            int lenghtPict = 72;
            int widthPict = 42;

            for (i = 0; i < 11; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (i != 2 && i != 5 && i != 8)
                    {
                        Coord tempCoord = new Coord(i * (lenghtPict + 10) + 40, j * (widthPict + 10) + 60);
                        Slot tempSlot = new Slot(m_slotList.Count, tempCoord);
                        m_slotList.Add(tempSlot);
                    }
                }
            }

            //Sorties
            Coord tempExitCoord = new Coord(2 * (lenghtPict + 10) + 50, 0);
            Exit exit1 = new Exit(0, tempExitCoord);
            tempExitCoord = new Coord(8 * (lenghtPict + 10) + 50 , 0);
            Exit exit2 = new Exit(1, tempExitCoord);
            tempExitCoord = new Coord(2 * (lenghtPict + 10) + 50, 10 * (widthPict + 10) + 80);
            Exit exit3 = new Exit(2, tempExitCoord);
            tempExitCoord = new Coord(8 * (lenghtPict + 10) + 50, 10 * (widthPict + 10) + 80);
            Exit exit4 = new Exit(3, tempExitCoord);
            m_exitList.Add(exit1);
            m_exitList.Add(exit2);
            m_exitList.Add(exit3);
            m_exitList.Add(exit4);
        }

        public int statsDisponible()
        {
            int resultat = 0;
            for (int i = 0; i < this.m_slotList.Count; i++)
            {
                if (this.m_slotList[i].state == SlotState.Empty)
                {
                    resultat++;
                }
            }
            return resultat;
        }

        public int statsOccupee()
        {
            int resultat = 0;
            for (int i = 0; i < this.m_slotList.Count; i++)
            {
                if (this.m_slotList[i].state == SlotState.Used)
                {
                    resultat++;
                }
            }
            return resultat;
        }

        public int statsReservee()
        {
            int resultat = 0;
            for (int i = 0; i < this.m_slotList.Count; i++)
            {
                if (this.m_slotList[i].state == SlotState.Reserved)
                {
                    resultat++;
                }
            }
            return resultat;
        }
       

    }
}