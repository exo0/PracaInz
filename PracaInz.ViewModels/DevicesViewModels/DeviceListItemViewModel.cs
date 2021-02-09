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
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceDescription { get; set; }
        public ICollection<Category> Categories { get; set; }
        public User DeviceOwner { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

    }
}
