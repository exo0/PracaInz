using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PracaInz.BLL;
using PracaInz.DAL.EF;
using PracaInz.ViewModels.AdministrationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PracaInz.Services
{
    public class UserRoleIdentityServices
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public UserRoleIdentityServices(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            RoleManager<Role> _roleManager,
            ApplicationDbContext _context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            context = _context;
            roleManager = _roleManager;
        }

        

        #region Users

        public UserListViewModel GetAllUsers()
        {
            var vm = new UserListViewModel
            {
                Users = context.Users.Select(x => new UserListItemViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Departemnt = x.Department,
                    Tickets = x.Tickets,
                    Devices = x.Devices
                })
            };
            return vm;
        }
        
        /// <summary>
        /// Funkcja która ma za zadanie zwrócić usera do widoku jako view model
        /// </summary>
        /// <param name="id"> Przekazujemy parametr ID danego usera którego będziemy edytować </param>
        /// <returns>Zwraca UserListItemViewModel który jest ładowany do View</returns>
        public UserListItemViewModel GetUser(int id)
        {
            var user = context.Users
                .Where(b => b.Id == id)
                .Include(c => c.Tickets)
                .Include(c => c.Devices)
                .FirstOrDefault();

            var vm = new UserListItemViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Departemnt = user.Department,
                Tickets = user.Tickets,
                Devices = user.Devices
            };

            return vm;
        }
        public User GetUserById(int id)
        {
            var user = context.Users.Find(id);

            return user;
        }

        //public void UpdateUser(id,firstName, lastName, department)
        //{
        //    var 
        //}

        public void DeleteUser(int id)
        {
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public IList<User> ReturnAllUsersToDropDown()
        {
            var usrs = context.Users.ToList();
            IList<User> users = new List<User>();

            foreach (var user in usrs)
            {
                users.Add(new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName + " " + user.LastName
                }) ;
            }

            return users;
        }
        #endregion

        #region Role
        public void AddRole()
        {

        }

        public RoleListViewModel GetAllRoles()
        {
            var vm = new RoleListViewModel
            {
                Roles = context.Roles.Select(x => new RoleListItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
            };
            return vm;
        }

        public Role GetRoleById(int id)
        {
            var role = context.Roles.Find(id);

            return role;
        }

        

        public void DeleteRole(int id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            context.SaveChanges();
        }

        #endregion
    }
}
