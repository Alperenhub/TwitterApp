using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TwitterApp.Data.Contexts;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var resultModel = new HomeViewModel();

            using (var context = new TwitterApp.Data.Contexts.TwttierAppDbContext())
            {
                var userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id")?.Value);
                var posts = context.Posts.Where(a => a.UserId == userID).ToList();

                var postViews = posts.Select(a => new Post()
                {
                    Id = a.Id,
                    Content = a.Content,
                    UserId = a.UserId,
                    CreateDate = a.CreateDate,
                    IsActive = a.IsActive,
                }).ToList();

                resultModel.Posts = postViews;
            }
            return View(resultModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
