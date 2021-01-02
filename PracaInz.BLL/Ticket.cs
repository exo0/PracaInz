using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.BLL
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool isFinished { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ClosedTime { get; set; }
        public User Author { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
    }
}
