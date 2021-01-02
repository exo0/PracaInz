using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.BLL
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
