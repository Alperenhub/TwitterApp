using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    public class AccountController : Controller
    {
        //  [Route("Account/Login")]
        //Routing işlemlerinde controller ve actionları kullanıyoruz. Bu parametreleri derleyici otomatik olarak atıyor.
        public IActionResult Login()
        {
            return View();
        }

      
        [HttpPost]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        public IActionResult RegisterAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAccountPost(RegisterAccountModel model)
        {

            using (var context = new TwitterApp.Data.Contexts.TwttierAppDbContext())
            {
                //var user = context.Users.FirstOrDefault(a => a.Username == model.Username && a.Password == model.Password && a.IsActive == true);

                context.Users.Add(new Data.Entities.User()
                {
                    CreateDate = DateTime.Now,
                    IsActive = true,
                    Name = model.Name,
                    Surname = model.Surname,
                    Username = model.Username,
                    Photo = "",
                    Password = model.Password,
                });
                context.SaveChanges();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult>  LoginPost(LoginViewModel model)

        {
            using (var context = new TwitterApp.Data.Contexts.TwttierAppDbContext())
            {
                var user = context.Users.FirstOrDefault(a => a.Username == model.Username && a.Password == model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("FullName", user.Name),
                        new Claim(ClaimTypes.Role, "Administrator"),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        // Refreshing the authentication session should be allowed.

                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTime.Now,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                   // return RedirectToAction("Index","Home");

                    return LocalRedirect("/");
                }
                else
                {
                    return View(model);
                }
            }
        }
    }
}
