﻿using Company.Data.Entities;
using Company.Service.Helper;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Company.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp

        
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if(ModelState.IsValid)
            {
                var role = new ApplicationUser
                {
                    UserName = input.Email.Split('@')[0],
                    Email = input.Email,
                    FirstName   = input.FirstName,
                    LastName =input.LastName,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(role, input.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));    
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(input);
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var role = await _userManager.FindByEmailAsync(input.Email);

                if (role is not null)
                {
                    if (await _userManager.CheckPasswordAsync(role, input.Password)) 
                    {
                        var result = await _signInManager.PasswordSignInAsync(role, input.Password , input.RememberMe , true );
                       
                        if (result.Succeeded)
                            return RedirectToAction("Index" , "Home");
                    }
                }
                ModelState.AddModelError("", "Incorrect Email Or PassWord");
                return View(input);

            }
            return View(input);
        }

		#endregion

		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
		}

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var role = await _userManager.FindByEmailAsync(input.Email);

                if (role is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(role);
                
                    var url = Url.Action("ResetPassword", "Account",new {Email = input.Email  ,Token = token } , Request.Scheme);

                    var email = new Email
                    {

                        Body = url,
                        Subject = "Reset Password",
                        To = input.Email
                    };

                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckEmail));
                }

            }

            return View(input);
        }


        public IActionResult CheckEmail()
        {
            return View();
        }

        public IActionResult ResetPassword(string email , string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var role = await _userManager.FindByEmailAsync(input.Email);

                if (role is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(role, input.Token , input.Password);
                   
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }   
            }

            return View(input);
        }

    }
}
