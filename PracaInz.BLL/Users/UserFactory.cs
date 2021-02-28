using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.BLL
{
    public class UserFactory
    {
        public static User Create(UserType usrType)
        {
            if(usrType == UserType.Admin)
            {
                return new Admin();
            }
            else if(usrType == UserType.HelpDesk)
            {
                return new HelpDesk();
            }
            else
            {
                return new User();
            }
        }


    }
}
