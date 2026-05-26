using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RepairRequestsBusinessLogic.Services;
using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.BusinessLogicsContracts;
using RepairRequestsContracts.SearchModels;
using RepairRequestsDataModels.Enums;
using System.Security.Claims;

namespace RepairRequestsWeb.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserLogic _userLogic;

        public AccountController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserBindingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserBindingModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                model.PasswordHash = PasswordService.getHash(model.Password);
                model.Role = UserRole.Пользователь;

                _userLogic.Create(model);
                TempData["Message"] = "Регистрация выполнена успешно. Теперь можно войти.";
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        public IActionResult Login()
        {
            return View(new UserBindingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserBindingModel model)
        {
            ModelState.Remove(nameof(model.Name));
            ModelState.Remove(nameof(model.PasswordHash));

            if (string.IsNullOrWhiteSpace(model.Login))
            {
                ModelState.AddModelError(nameof(model.Login), "Введите логин");
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError(nameof(model.Password), "Введите пароль");
            }

            if (!ModelState.IsValid) return View(model);

            var passHash = PasswordService.getHash(model.Password);

            var user = _userLogic.ReadElement(new UserSearchModel
            {
                Login = model.Login,
                PasswordHash = passHash,
            });

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Login", user.Login)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal
            );

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
