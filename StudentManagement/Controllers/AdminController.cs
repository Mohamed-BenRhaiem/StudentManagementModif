using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models.ViewModels;
using System.Data;

namespace StudentManagement.Controllers
{
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Users(string roleName)
        {
            var users = await _userManager.Users.ToListAsync();
            // Inside AdminController
          
                // Pass roleName to the view
                ViewBag.RoleName = roleName;
                // Other logic to fetch users based on roleName
                return View(users);
            
        }
        [HttpPost]
        public async Task<IActionResult> addUser(string roleName,string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            // Normalize the role name
            // Check if the user is already in the role
            var isInRole = await _userManager.IsInRoleAsync(user, roleName);

            if (!isInRole)
            {
                // If the user is not already in the role, add them to the role
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return RedirectToAction("ListeRoles");
        }
        // Role ID is passed from the URL to the action
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            // Retrieve all the Users
            foreach (var user in _userManager.Users.ToList())
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("ListeRoles");
            }
            else
            {
                role.Name = model.RoleName;
                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListeRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }

                return View(model);
            }
        }



        // GET: AdminController
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName};
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
            }

            return View(model);
        }
        
        
        //Get All Roles
        [HttpGet]
        public IActionResult ListeRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }




        // GET: AdminController/Create
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

       
    }
}
