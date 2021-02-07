﻿using PracaInz.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.AdministrationViewModels
{
    public class UserListItemViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Departemnt { get; set; }
        public IList<Ticket> Tickets { get; set; }
        public IList<Device> Devices { get; set; }


    }
}
