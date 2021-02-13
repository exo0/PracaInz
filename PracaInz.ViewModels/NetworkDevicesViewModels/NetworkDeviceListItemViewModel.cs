using PracaInz.ViewModels.DevicesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.NetworkDevicesViewModels
{
    public class NetworkDeviceListItemViewModel : DeviceListItemViewModel
    {
        public string IPAddress { get; set; }
    }
}
