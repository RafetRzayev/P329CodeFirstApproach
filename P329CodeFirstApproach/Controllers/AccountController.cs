﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P329CodeFirstApproach.Data;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.Services;
using P329CodeFirstApproach.ViewModels;

namespace P329CodeFirstApproach.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User
            {
                Fullname = model.Fullname,
                Email = model.Email,
                UserName = model.Username,
            };

            var existUsername = await _userManager.FindByNameAsync(model.Username);

            if (existUsername != null)
            {
                ModelState.AddModelError("Username", "Bu add username movcuddur");
                return View();
            }

            //var roleResult = await _roleManager.CreateAsync(new IdentityRole
            //{
            //    Name = "Admin"
            //});

            //if (!roleResult.Succeeded)
            //{
            //    foreach (var item in roleResult.Errors)
            //    {
            //        ModelState.AddModelError("", item.Description);
            //    }

            //    return View();
            //}

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                //await _userManager.AddToRoleAsync(user, "Admin");

                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await _userManager.FindByNameAsync(model.Username);

            if (existUser == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");

                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(existUser, model.Password, false, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "You banned");

                return View();
            }

            if (!result.Succeeded)
            {
                return View();
            }

            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(model.ReturnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var username = User?.Identity?.Name;
            if (username == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(username);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu email-nen istifadece qeydiyyatdan kecmeyib");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action(nameof(ResetPassword), "Account", new { email = model.Email, token = token }, Request.Scheme, Request.Host.ToString());

            var requestMail = new RequestEmail
            {
                ToEmail = model.Email,
                Subject = "Reset password",
                Body = resetLink
            };

            await _mailService.SendEmailAsync(requestMail);
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel
            {
                Email=email,
                Token=token
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bele email yoxdu");
                return View();
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(Login));
        }
    }
}
