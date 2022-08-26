using CarRentalApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalApp.Controllers
{
    public class UserController : Controller
    {
        public static List<User> people = new List<User>
        {
            new User("tom@gmail.com", "12345"),
            new User("bob@gmail.com", "55555")
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {


            User? userInDb = people.FirstOrDefault(p => p.Email == user.Email && p.Password == user.Password);
            if (userInDb is null) return Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, userInDb.Email) };

            if (userInDb.Email == "tom@gmail.com")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Redirect("/");
        }
    }
}

