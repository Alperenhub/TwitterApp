namespace TwitterApp.Models
{
    public class HomeViewModel
    {
        public List<Post> Posts { get; set; }

        public HomeViewModel()
        {
            Posts = new List<Post>();
        }
    }
}
