using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Dapper.Data.Logic;
using Dapper.Data.Models;
using Dapper.Service.Interfaces;
using Dapper.ViewModel;
using Dapper.Web.Helpers;

namespace Dapper.Web.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAccountService _accountService;
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginBindingModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                HandleFailResult(GetModalStateErrors());
                return View(model);
            }

            var loginResult = _accountService.Login(model.Email, model.Password);
            if (loginResult.IsFailure)
            {
                HandleFailResult(loginResult.Errors);
                return View(model);
            }

            var account = loginResult.Value;
            SignInCookie(account);

            if (!string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToLocal(model.ReturnUrl);
            
         

            return RedirectToAction("Index", "Home");
        }

        private void SignInCookie(Account account)
        {
            var claimIdentity = account.CreateClaimSignIn();
            ApplicationSignInManager.AuthenticationManager.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.Now.AddDays(10)
            }, claimIdentity);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var model =new RegisterBindingModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                HandleFailResult(GetModalStateErrors());
                return View(model);
            }

            var registerResult =_accountService.Create(model);
            if (registerResult.IsFailure)
            {
                HandleFailResult(registerResult.Errors);
                return View(model);
            }

            var account = registerResult.Value;
            var mailer = new Mailer();
            var urlVerifyEmail =
                $"{Domain}{Url.Action("ConfirmEmail", "Auth", new {token = account.TokenConfrimEmail})}";
            mailer.ActiveAccount(account.Email, urlVerifyEmail).Send();
            HandleSuccessResult("Register successful");

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            ApplicationSignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login","Auth");
        }

        [HttpGet]
        public ActionResult ConfirmEmail(string token)
        {
            var result = _accountService.ConfirmEmail(token);
            if (result.IsFailure)
            {
                HandleFailResult(result.Errors);
                return RedirectToAction("Register","Auth");
            }

            HandleSuccessResult("Confirm email successful");
            return RedirectToAction("Login");
        }
    }
}