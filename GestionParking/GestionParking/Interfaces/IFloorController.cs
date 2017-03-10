using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    interface IFloorController
    {
        void generateFloor1();
        void generateFloor2();
        void generateFloor3();
        int statsDisponible();
        int statsOccupee();
        int statsReservee();
    }
}
