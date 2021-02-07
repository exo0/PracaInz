using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.AdministrationViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        public bool IsSelected { get; set; }
    }
}
