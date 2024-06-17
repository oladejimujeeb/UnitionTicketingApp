using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnitionTicketingApp.DTO;
using UnitionTicketingApp.Implementation.Service;
using Microsoft.AspNetCore.Identity;

namespace UnitionTicketingApp.Controllers
{

    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly SignInManager<IdentityUser> _signInManager;


        public UserController(UserService userService, SignInManager<IdentityUser> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();                
            }
            var login = await _userService.SignIn(model);
            if(!login.Status)
            {
                ViewBag.Message = login.Message;
                return View();
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult SignUp()
        {
            var role = _userService.AllRoles().Data;
            ViewData["Roles"] = new SelectList(role, "RoleName", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var createUser = await _userService.CreateUser(model);
            if (!createUser.Status)
            {
                ViewBag.Message = createUser.Message;
                return View();
            }
            return RedirectToAction("AllBugs", "Bug");
          
        }
        [HttpGet]
        public async Task< IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
