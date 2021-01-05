using PracaInz.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.DevicesViewModels
{
    public class DeviceListItemViewModel
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public ICollection<Category> Categories { get; set; }
        public int CategoryId { get; set; }

    }
}
