using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.AdministrationViewModels
{
    public class RoleListViewModel
    {
        public IEnumerable<RoleListItemViewModel> Roles { get; set; }
    }
}
