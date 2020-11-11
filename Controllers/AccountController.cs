using System.Threading.Tasks;
using CinemaPortal_ASP.NET_Core.Helpers;
using CinemaPortal_ASP.NET_Core.Models;
using CinemaPortal_ASP.NET_Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPortal_ASP.NET_Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.UserName };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь с таким e-mail не найден. Проверьте введенные данные, либо пройдите процедуру регистрации.");
                    return View(model);
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // проверяем, принадлежит ли URL приложению
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный пароль");
                    }
                }
                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null || (await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset Password",
            $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        // GET: /Manage/Index
        [HttpGet]
        public async Task<ActionResult> Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangeUserNameSuccess ? "Ваше имя изменено."
                : message == ManageMessageId.ChangeEmailSuccess ? "Ваш email изменен."
                : message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : "";
            
           
             var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();
            var model = new ManageAccountViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                return View(model);
            

        }

        // GET: /Manage/ChangeUserName
        [HttpGet]
        public ActionResult ChangeUserName()
        {
            ChangeUserNameViewModel model = new ChangeUserNameViewModel { OldUserName = User.Identity.Name };
            return View(model);
        }
 
        // POST: /Manage/ChangeUserName
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserName(ChangeUserNameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await _userManager.GetUserAsync(HttpContext.User);
                if(user!=null)
                {
                    user.UserName = model.NewUserName;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded) 
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangeUserNameSuccess }); 
                    }
                    else return RedirectToAction("Manage", new { Message = ManageMessageId.Error });

                }
                else return NotFound();
        }
        
        // GET: /Manage/ChangeEmail
        [HttpGet]
        public async Task<ActionResult> ChangeEmail()
        {
            ChangeEmailViewModel model = new ChangeEmailViewModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.OldEmail = user.Email;
            return View(model);
        }
        // POST: /Manage/ChangeEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.OldEmail = user.Email;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (user != null)
            {
                user.Email = model.NewEmail;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangeEmailSuccess });
                }

                else return RedirectToAction("Manage", new { Message = ManageMessageId.Error });

            }
            else return RedirectToAction("Manage", new { Message = ManageMessageId.Error });

        }
        
        // GET: /Manage/ChangePassword
        [HttpGet]
        public ActionResult ChangePassword()
        {
             return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (user != null)
            {
                var _passwordValidator =
                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
                IdentityResult result =
                await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                if (result.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                else return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            else return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        public ActionResult CancelEdit()
        {
            return RedirectToAction("Manage");
        }
        public enum ManageMessageId
        {
            ChangeUserNameSuccess,
            ChangeEmailSuccess,
            ChangePasswordSuccess,
            Error
        }

        
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
