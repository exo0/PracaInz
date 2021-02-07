using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.DevicesViewModels
{
    public class NewDeviceViewModel
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceDescription { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

    }
}
