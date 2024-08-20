using AutoMapper;
using MBEAUTY.Helpers.Enums;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IFileService fileService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _fileService = fileService;
            _mapper = mapper;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM request)
        {
            if (!ModelState.IsValid) return View(request);

            AppUser newUser = _mapper.Map<AppUser>(request);

            IdentityResult result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

                return View(request);
            }

            await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token },
                Request.Scheme, Request.Host.ToString());

            string body = string.Empty;

            body = _fileService.ReadFile("wwwroot/templates/verify.html", body);

            body = body.Replace("{{link}}", link);
            body = body.Replace("{{fullname}}", newUser.FullName);

            _emailService.Send(null, newUser.Email, body, "Verify email");

            return RedirectToAction(nameof(CheckEmail));
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM request)
        {
            if (!ModelState.IsValid) return View(request);

            AppUser user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            user ??= await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong!");
                return View(request);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View(request);
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM request)
        {
            if (!ModelState.IsValid) return View(request);

            AppUser existUser = await _userManager.FindByEmailAsync(request.Email);

            if (existUser is null)
            {
                ModelState.AddModelError("Email", "User not found!");
                return View(request);
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());

            string body = string.Empty;

            body = _fileService.ReadFile("wwwroot/templates/verify.html", body);

            body = body.Replace("{{link}}", link);
            body = body.Replace("{{fullname}}", existUser.FullName);

            _emailService.Send(null, existUser.Email, body, "Reset password");

            return RedirectToAction(nameof(CheckEmail));
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { Token = token, UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);

            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);

            if (existUser == null) return NotFound();

            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "This password already exist!");
                return View(resetPassword);
            }

            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);

            return RedirectToAction(nameof(SignIn));
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CheckEmail()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId is null || token is null) return RedirectToAction("NotFound", "Error");

            AppUser existUser = await _userManager.FindByIdAsync(userId);

            if (existUser is null) return RedirectToAction("NotFound", "Error");

            await _userManager.ConfirmEmailAsync(existUser, token);

            await _signInManager.SignInAsync(existUser, false);

            return RedirectToAction(nameof(SignIn));
        }

        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }
    }
}
