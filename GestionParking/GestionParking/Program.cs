using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parking;
using Parking.Interfaces;
using Parking.Controllers;

namespace Parking
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Form1 view = new Form1();
            Floor mdlFloor1 = new Floor(1,new List<Slot>());
            Floor mdlFloor2 = new Floor(2, new List<Slot>());
            Floor mdlFloor3 = new Floor(3, new List<Slot>());
            Floor mdlFloor4 = new Floor(4, new List<Slot>());
            Floor mdlFloor5 = new Floor(5, new List<Slot>());
            IFloorController cntFloor1 = new FloorController(view, mdlFloor1);
            IFloorController cntFloor2 = new FloorController(view, mdlFloor2);
            IFloorController cntFloor3 = new FloorController(view, mdlFloor3);
            IFloorController cntFloor4 = new FloorController(view, mdlFloor4);
            IFloorController cntFloor5 = new FloorController(view, mdlFloor5);
            Application.Run(view);
        }
    }
}
