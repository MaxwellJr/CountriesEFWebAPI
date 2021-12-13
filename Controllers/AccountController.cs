using CountriesEFWebAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CountriesEFWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly CountriesContext context;

        public AccountController(CountriesContext dbContext) {
            context = dbContext;
        }

        private async Task Authenticate(string userName) {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public string HashPassword(string password) {
            if (password == null) {
                throw new ArgumentNullException("Incorrect password");
            }
            byte[] buffer, salt;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 16, 1000)) {
                buffer = bytes.GetBytes(32);
                salt = bytes.Salt;
            }
            byte[] dst = new byte[49];
            Buffer.BlockCopy(buffer, 0, dst, 1, 32);
            Buffer.BlockCopy(salt, 0, dst, 33, 16);
            return Convert.ToBase64String(dst);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password) {
            if (hashedPassword == null) {
                return false;
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 49) || (src[0] != 0)) {
                return false;
            }
            byte[] passwordBytes = new byte[32];
            Buffer.BlockCopy(src, 1, passwordBytes, 0, 32);
            byte[] salt = new byte[16];
            Buffer.BlockCopy(src, 33, salt, 0, 16);
            byte[] buffer;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, salt, 1000)) {
                buffer = bytes.GetBytes(32);
            }
            return passwordBytes.SequenceEqual(buffer);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginModel model) {
            if (ModelState.IsValid) {
                User user = await context.Users.SingleOrDefaultAsync(u => u.Login == model.Login);
                if (user != null && VerifyHashedPassword(user.Password, model.Password)) {
                    await Authenticate(model.Login);
                    return Ok(user);
                } else {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignUp(LoginModel model) {
            if (ModelState.IsValid) {
                User user = await context.Users.SingleOrDefaultAsync(u => u.Login == model.Login);
                if (user == null) {
                    string hashedPassword = HashPassword(model.Password);
                    user = new User(model.Login, hashedPassword);
                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                    await Authenticate(model.Login);
                    return Ok(user);
                } else {
                    ModelState.AddModelError("", "User with the same login already exists");
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<ActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
