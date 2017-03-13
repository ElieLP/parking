using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface ISlotController
    {
        void NewState(SlotState p_s);
    }
}
