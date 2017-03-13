using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Interfaces;
using Parking.Views;

namespace Parking.Controllers
{
    class SlotController : ISlotController
    {
        IView view;
        Slot model;

        public void NewState(SlotState p_s)
        {
            throw new NotImplementedException();
        }
    }
}
