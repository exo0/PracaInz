using PracaInz.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.NetworkDevicesViewModels
{
    public class NetworkDeviceListViewModel
    {
        public IEnumerable<NetworkDeviceListItemViewModel> NetworkDevices { get; set; }
    }
}
