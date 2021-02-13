using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracaInz.BLL;
using PracaInz.Services;
using PracaInz.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Web.Controllers
{
    public class TicketController : Controller
    {
        private TicketServices _ticketService;
        private UserManager<User> _userManager;

        public TicketController(TicketServices ticketServices, UserManager<User> userManager)
        {
            _ticketService = ticketServices;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vm = _ticketService.GetAllTickets();
            return View(vm);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vm = _ticketService.GetTicket(id);
            return View(vm);
        }
        #region TicketStatus
        
        public IActionResult AssignTicketToYourself(int id)
        {
            _ticketService.AssignTicketToYourSelf(id);
            return RedirectToAction("Index", "Ticket");
        }

        public IActionResult FinishTicketSuccesfully(int id)
        {
            _ticketService.FinishTicketSuccesfully(id);
            return RedirectToAction("Index", "Ticket");
        }

        public IActionResult FinishTicketUnresolved(int id)
        {
            _ticketService.FinishTicketUnresolved(id);
            return RedirectToAction("Index", "Ticket");
        }

        public IActionResult PostponeTicket(int id)
        {
            _ticketService.PostponeTicket(id);
            return RedirectToAction("Index", "Ticket");
        }

        #endregion


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _ticketService.GetTicket(id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _ticketService.DeleteTicket(id);
            return RedirectToAction("Index", "Ticket");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> AddAsync(NewTicketViewModel data)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(currentUser);

            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _ticketService.AddTicket(data.Title,
                data.Message,
                user.Id);
            return RedirectToAction("Index", "Ticket");
        }

    }
}
