using Microsoft.AspNetCore.Mvc;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    public class AccountController1 : Controller
    {
      //  [Route("Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        [Route("Account/RegisterAccount")]
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

            return View();
        }
    }
}
