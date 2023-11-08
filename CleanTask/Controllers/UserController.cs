using CleanTask.Tools;
using CleanTask.Domains;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Infrastructure.Data.Entitties;
using Infrastructure.Data;

namespace CleanTask.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.IsSent = false;
            return View();
        }
        private readonly UserManager<CleanTask.Domains.userapp> _userManager;
        private readonly SignInManager<CleanTask.Domains.userapp> _signInManager;
        private readonly MyDBContex _contex;
        private readonly IEmailSend _emailSend;
        private readonly IViewRenderService _viewRenderService;
        public UserRepositories _userRepositories;
        public UserController(UserRepositories userRepositories ,UserManager<CleanTask.Domains.userapp> userManager, SignInManager<CleanTask.Domains.userapp> signInManager, MyDBContex contex)
        {
            _userRepositories = userRepositories;
            _contex = contex;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewBag.ReturnUrl = returnUrl;
            return View("/user/login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(Domains.LoginModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "کاربری با این مشخصات یافت نشد");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "تلاش برای ورود نامعتبر است!");
            }

            return View("/user/login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOute()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [Route("/api/User/create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(Domains.UserModel u)
        {
            if (!ModelState.IsValid)
            { return View(); }
            //UserBLL bll = new UserBLL();
            if (u.Password == u.Password_Again)
            {
                var user = new CleanTask.Domains.userapp
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneUser,
                    firstname = u.Name
                };
                Domains.UserMe use = new Domains.UserMe();
                use.Email = u.Email;
                use.UserName = u.UserName;
                use.Name = u.Name;
                use.PhoneUser = u.PhoneUser;
                use.IsRegister = true;
                use.Password = u.Password;
                _userRepositories.Create(use);
                _contex.SaveChanges();
                var addresult = await _userManager.CreateAsync(user, u.Password);
                if (!addresult.Succeeded)
                {
                    foreach (var err in addresult.Errors)
                    {
                        ModelState.AddModelError(key: string.Empty, errorMessage: err.Description);
                        return View();
                    }
                }
                var uuser = await _userManager.FindByNameAsync(u.UserName);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(uuser);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                string? callBackUrl = Url.ActionLink("confirmemail", "account", new { userId = uuser.Id, token = token }, Request.Scheme);
                string body = await _viewRenderService.RenderToStringAsync("_RegisterEmail", callBackUrl);
                await _emailSend.SendEmailAsync(new EmailModel(uuser.Email, "تایید حساب", body));
                ViewBag.IsSent = true;
            }

            return View("../Home/Index");



        }

        //SendEmail
        #region Remote Validations

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsAnyUserName(string username)
        {
            bool any = await _userManager.Users.AnyAsync(i => i.UserName == username);
            if (!any)
                return Json(true);

            return Json("نام کاربری تکراری است");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsAnyEmail(string email)
        {
            bool any = await _userManager.Users.AnyAsync(i => i.Email == email);
            if (!any)
                return Json(true);

            return Json(" ایمیل تکراری است");

        }
        #endregion
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            ViewBag.IsConfirmed = result.Succeeded ? true : false;
            return View("~/");
        }
    }
}
