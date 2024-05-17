using e_shop.Data;
using e_shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Services;
using WebApplication1.viewModel;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,AppDbContext context, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _accountService = accountService;
            
        }
        public IActionResult Login()
        {
            var response=new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) { return View(loginViewModel); }
            var user=await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                //User is found, check passowrd
                var passwordCheck=await _userManager.CheckPasswordAsync(user,loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Books");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong Credentials.Please try again.";
                return View(loginViewModel);
            }
            //User is not found
            TempData["Error"] = "Wrong Credentials.Please try again.";
            return View(loginViewModel);
        }
        private void MapUserEdit(AppUser user, EditUserProfileViewModel editVM)
        {
            user.Id = editVM.Id;
            user.UserName = editVM.Username;

        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid) return View(registerViewModel);
            var user=await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use.";
                return View(registerViewModel);
            }
            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
                
            };
            var newUserResponse=await _userManager.CreateAsync(newUser,registerViewModel.Password);
            if(newUserResponse.Succeeded) 
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        public async Task<IActionResult>Logout()
        {
            await _signInManager.SignOutAsync();
             return RedirectToAction("Index", "Books");
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _accountService.GetCurrentUserId();
            var user = await _accountService.GetUserById(curUserId);
            if (user == null)
            {
                return View("Error");
            }
            var editUserProfileVM = new EditUserProfileViewModel()
            {
                Id = curUserId,
                Username = user.UserName,
            };
            return View(editUserProfileVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _accountService.GetUserByIdNoTracking(editVM.Id);
            if (!await _userManager.CheckPasswordAsync(user, editVM.CurrentPassword))
            {
                TempData["Error"] = "Wrong password";
                return RedirectToAction("EditUserProfile", editVM);
            }

            if (editVM.NewPassword != null)
            {
                var newPass = await _userManager.ChangePasswordAsync(user, editVM.CurrentPassword, editVM.NewPassword);
                if (!newPass.Succeeded)
                {
                    TempData["Error"] = "Failed changing password";
                    return RedirectToAction("EditUserProfile", editVM);
                }
            }

            MapUserEdit(user, editVM);
            _accountService.Update(user);
            return RedirectToAction("Index", "Book");
        }
    }
}
