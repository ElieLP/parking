using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Interfaces;
using Parking.Views;

namespace Parking.Controllers
{
    public class FloorController : IFloorController
    {
        IView view;
        Floor model;
        public FloorController(IView v, Floor m)
        {
            view = v;
            model = m;
            view.setController(this);
            //model.
        }

        public void generateFloor1()
        {
            model.generateFloor1();
        }

        public void generateFloor2()
        {
            model.generateFloor2();
        }

        public void generateFloor3()
        {
            model.generateFloor3();
        }

        public int statsDisponible()
        {
            return model.statsDisponible();
        }

        public int statsOccupee()
        {
            return model.statsOccupee();
        }

        public int statsReservee()
        {
            return model.statsReservee();
        }
        
    }
}
