using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.BLL
{
    public class NetworkDevice : Device
    {
        public string IPAddress { get; set; }
        
        public bool isAlive { get; set; }

        // miejsce na metode która ma pingować 
    }
}
