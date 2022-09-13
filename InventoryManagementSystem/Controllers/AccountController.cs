using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Interfaces;

namespace InventoryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ISendGrid _sendGrid;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ISendGrid sendGrid)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sendGrid = sendGrid;
        }

        [Authorize]
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            RegisterViewModel viewModel = new();
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel, string? returnUrl = null)
        {
            viewModel.ReturnUrl = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");



            if (ModelState.IsValid)
            {
                var user = new IdentityUser { Email = viewModel.Email , UserName = viewModel.UserName};
                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login");
                }
                else
                    ModelState.AddModelError("Password", "User could not be created");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            LoginViewModel viewModel = new();
            viewModel.ReturnUrl = returnUrl ?? Url.Content("~/");
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginViewModel viewModel, string? returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return RedirectToAction("ForgotPasswordConfirmation");
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code = code}, protocol: HttpContext.Request.Scheme);

                await _sendGrid.SendEmailAsync(model.Email, "Reset Email Confirmation", "Please reset email by going to this " +
                    "<a href=\"" + callbackurl + "\">link</a>");

                return RedirectToAction("ForgotPasswordConfirmation");

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    ModelState.AddModelError("Email", "User not found");
                    return View();
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("ResetPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}