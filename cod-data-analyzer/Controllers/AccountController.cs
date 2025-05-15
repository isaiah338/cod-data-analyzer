using cod_data_analyzer.Models;
using cod_data_analyzer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace cod_data_analyzer.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AccountController(SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User) ?? new ApplicationUser();
            string userName = user.UserName ?? "";
            AccountHomeViewModel viewModel = new() { 
                AccountName = userName,
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Login(String returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = "/Account/Index")
        {
            
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginViewModel.UserName);

                if (user != null)
                {
                    // sign out the user if they are logged in
                    await _signInManager.SignOutAsync();

                    // verify proper password entered
                    Microsoft.AspNetCore.Identity.SignInResult signInResult =
                           await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        // this will redirect user to page they
                        // were attempting to access before the 
                        // login attempt, if null redirect to 
                        // application root
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(loginViewModel.UserName), "Invalid user or password");
                }
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = userViewModel.UserName
                };

                IdentityResult result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userViewModel);
        }
    }

}
