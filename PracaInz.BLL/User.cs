using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.BLL
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
        public virtual IList<Device> Devices { get; set; }
        public virtual IList<NetworkDevice> NetworkDevices { get; set; }
        
    }
}
