using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracaInz.BLL;
using PracaInz.Services;
using PracaInz.ViewModels.AdministrationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private UserRoleIdentityServices userRoleServices;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        public AdministrationController(UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            RoleManager<Role> _roleManager,
            UserRoleIdentityServices _userRoleServices)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
            userRoleServices = _userRoleServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Index()
        {
            var vm = userRoleServices.GetAllUsers();
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var usr = UserFactory.Create(model.UserType);

                usr.UserName = model.Email;
                usr.Email = model.Email;
                usr.FirstName = model.FirstName;
                usr.LastName = model.LastName;

                // Kopiowanie danych z modelu RegisterUserViewModel do... chcemy by tutaj był user, lecz nie chce byc 
                

                // Zapisanie danych usera do bazy 
                
                var result = await userManager.CreateAsync(usr, model.Password);

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult IndexRole()
        {
            var vm = userRoleServices.GetAllRoles();
            return View(vm);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RegisterRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                var identityRole = new Role
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                var result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {

            var role = userRoleServices.GetRoleById(model.Id);



            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
            {
                var vm = userRoleServices.GetRoleById(id);
                return View(vm);
            }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var vm = userRoleServices.GetUser(id);
            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditUserConfirmed(int id, string firstName, string lastName, string department)
        {
            userRoleServices.UpdateUser(id, firstName, lastName, department);
            return RedirectToAction("Index", "Administration");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteUser(int id)
        {
            var vm = userRoleServices.GetUserById(id);
            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteUserConfirmed(int id)
        {
            userRoleServices.DeleteUser(id);
            return RedirectToAction("Index", "Administration");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id)
            {
            userRoleServices.DeleteRole(id);
                return RedirectToAction("Index", "Role");
            }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
            public async Task<IActionResult> EditUsersInRole(string roleId)
            {
                ViewBag.roleId = roleId;

                var role = await roleManager.FindByIdAsync(roleId);
                var model = new List<UserRoleViewModel>();

                foreach (var user in userManager.Users)
                {
                    var userRoleViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id.ToString(),
                        UserName = user.UserName

                    };

                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    else
                    {
                        userRoleViewModel.IsSelected = false;
                    }

                    model.Add(userRoleViewModel);
                }

                return View(model);
            }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
            public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
            {
                var role = await roleManager.FindByIdAsync(roleId);
                for (int i = 0; i < model.Count; i++)
                {
                    var user = await userManager.FindByIdAsync(model[i].UserId);

                    IdentityResult result = null;

                    if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }

                    if (result.Succeeded)
                    {
                        if (i < (model.Count - 1))
                            continue;
                        else
                            return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }

                return RedirectToAction("EditRole", new { Id = roleId });
            }
        }
    }

