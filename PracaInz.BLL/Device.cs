using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.BLL
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public Category CategoryDevice { get; set; }
        public User DeviceOwner { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
