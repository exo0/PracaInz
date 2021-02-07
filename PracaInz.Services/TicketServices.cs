using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracaInz.BLL;
using PracaInz.DAL.EF;
using PracaInz.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.Services
{
    public class TicketServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public TicketServices(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        public TicketListViewModel GetAllTickets()
        {
            var vm = new TicketListViewModel()
            {
                Tickets = _context.Tickets.Select(x => new TicketListItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Message = x.Message,
                    Status = x.Status,
                    CreateTime = x.CreateTime,
                    ClosedTime = x.ClosedTime,
                    Author = x.Author
                })
            };

            return vm;
        }

        public TicketListItemViewModel GetTicket(int id)
        {

            var Ticket = _context.Tickets
                .Where(b => b.Id == id)
                .Include(b => b.Author)
                .FirstOrDefault();

            var vm = new TicketListItemViewModel
            {
                Id = Ticket.Id,
                Title = Ticket.Title,
                Message = Ticket.Message,
                Status = Ticket.Status,
                CreateTime = Ticket.CreateTime,
                ClosedTime = Ticket.ClosedTime,
                Author = Ticket.Author,
                AuthorId = Ticket.AuthorId

            };
            return vm;
        }

        public void Add(string title, string message, TicketStatus status)
        {
            var Ticket = new Ticket
            {
                Title = title,
                Message = message,
                Status = status,
                CreateTime = DateTime.Now

            };
            _context.Tickets.Add(Ticket);
            _context.SaveChanges();
        }
    }
}
