using Microsoft.AspNetCore.Mvc;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    public class PostController : Controller
    {

        public PostController() { }


        public IActionResult AddPost() { return View(); }

        [HttpPost]
        public IActionResult AddPost(Post model)
        {

            using (var context = new TwitterApp.Data.Contexts.TwttierAppDbContext())
            {

                var postEntity = new Data.Entities.Post()
                {

                    CreateDate = DateTime.Now,
                    IsActive = true,
                    Content = model.Content,
                    Id = model.Id,
                    UserId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id")?.Value)
                };
                context.Posts.Add(postEntity);

                context.SaveChanges();
            }

            return LocalRedirect("/");
        }
    }
}
