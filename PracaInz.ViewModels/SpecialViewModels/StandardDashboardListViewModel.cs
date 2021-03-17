using PracaInz.ViewModels.DevicesViewModels;
using PracaInz.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.SpecialViewModels
{
    // This class is giving us access to two viewmodels in the same time. I'm using this in Dashboard section
    public class StandardDashboardListViewModel
    {
        public IEnumerable<TicketListItemViewModel> Tickets { get; set; }
        public IEnumerable<DeviceListItemViewModel> Devices { get; set; }
    }
}
