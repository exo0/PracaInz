﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public TicketServices(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
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

        public TicketListViewModel GetAllOpenTicketsFilteredByUserId(string userName)
        {
            var vm = new TicketListViewModel()
            {
                Tickets = _context.Tickets.Where(x=>x.Author.UserName == userName)
                .Where(x=>x.Status != TicketStatus.Finished)
                .Select(x => new TicketListItemViewModel
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

        public TicketListViewModel GetAllFinishedTicketsFilteredByUserId(string userName)
        {
            var vm = new TicketListViewModel()
            {
                Tickets = _context.Tickets.Where(x => x.Author.UserName == userName)
                .Where(x => x.Status == TicketStatus.Finished)
                .Select(x => new TicketListItemViewModel
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
            var vm1 = _mapper.Map<TicketListItemViewModel>(Ticket);
            return vm1;
        }

        public async Task AddTicket(string title, string message,int userId)
        {
            User usr = _context.Users.Find(userId);
            var Ticket = new Ticket
            {
                Title = title,
                Message = message,
                Status = (TicketStatus)0,
                CreateTime = DateTime.Now,       
                Author = usr


            };
            _context.Tickets.Add(Ticket);
            _context.SaveChanges();
        }

        public void DeleteTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            
        }

        #region TicketStatus
        public void AssignTicketToYourSelf(int id)
        {

            var ticket = _context.Tickets.Find(id);
            ticket.Status = (TicketStatus)1;
            
            _context.Tickets.Update(ticket);
            
            _context.SaveChanges();
        }

        public void FinishTicketSuccesfully(int id)
        {
            var ticket = _context.Tickets.Find(id);
            ticket.Status = (TicketStatus)2;
            ticket.ClosedTime = DateTime.Now;
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
        }

        public void FinishTicketUnresolved(int id)
        {
            var ticket = _context.Tickets.Find(id);
            ticket.Status = (TicketStatus)3;
            ticket.ClosedTime = DateTime.Now;
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
        }

        public void PostponeTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);
            ticket.Status = (TicketStatus)4;
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
        }
        #endregion



    }
}
