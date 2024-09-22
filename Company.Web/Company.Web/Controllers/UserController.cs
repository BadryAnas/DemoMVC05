using Company.Data.Entities;
using Company.Service.Interfaces.Employee.Dto;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize]

    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserController(UserManager<ApplicationUser> userManager , ILogger<UserController> logger)
        {
			_userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string searchInp)
		{
			List<ApplicationUser> users  = new List<ApplicationUser>();
			if(string.IsNullOrEmpty(searchInp))
				users = await _userManager.Users.ToListAsync();
			else 
				users = await _userManager.Users
					.Where(role => role.NormalizedEmail.Trim().Contains(searchInp.Trim().ToUpper()))
					.ToListAsync();

			return View(users);
		}

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            var role
                = await _userManager.FindByIdAsync(id);

            if ( role is null)
            {
                return NotFound();
            }


            if (viewName == "Update")
            {
                var userUpdateViewModel = new UserUpdateViewModel
                {
                    Id = role.Id,
                    UserName = role.UserName
                };
                
                return View(viewName, userUpdateViewModel);
            }



            return View(viewName, role);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }


        [HttpPost]
        public async Task<IActionResult> Update(string id, UserUpdateViewModel applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _userManager.FindByIdAsync(id);

                    if (role is null)
                    {
                        return NotFound();
                    }

                    role.UserName = applicationUser.UserName;
                    role.NormalizedEmail = applicationUser.UserName.ToUpper();

                    var result =  await _userManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Succesfully");
                        return RedirectToAction("Index");
                    }

                    foreach (var item in result.Errors)
                    {
                        _logger.LogError(item.Description);
                    }

                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);

                }

            }

            return View(applicationUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _userManager.FindByIdAsync(id);

                if (role is null)
                {
                    return NotFound();
                }

                var result = await _userManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    _logger.LogError(item.Description);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return RedirectToAction("Index");

        }

    }
}
