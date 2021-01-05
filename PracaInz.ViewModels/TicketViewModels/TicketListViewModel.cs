using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.TicketViewModels
{
    public class TicketListViewModel
    {
        public IEnumerable<TicketListItemViewModel> Tickets { get; set; }
    }
}
