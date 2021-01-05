using PracaInz.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.TicketViewModels
{
    public class TicketListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ClosedTime { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
    }
}
