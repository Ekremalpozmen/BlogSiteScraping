using BlogSite.Services.WebSite.HomePage;
using BlogSite.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly HomePageService _homePageService;
        public HomeController(HomePageService homePageService)
        {
            _homePageService = homePageService;
        }


        public async Task<ActionResult> Index(BlogModel model)
        {
            var result = await _homePageService.GetProducts(model);
            return View(result);
        }
        //public ActionResult BlogDetay(int id)
        //{
        //    var blog = db.Blogs.Where(x => x.Id == id);
        //    var blogdetay = db.Blogs.Where(x => x.Id == id).FirstOrDefault();
        //    //var blogdetay = blog.Select(x => new BlogModel()
        //    //{
        //    //    Id=x.Id,
        //    //    Description = x.Description,
        //    //    ImageUrl = x.ImageUrl,
        //    //    Title = x.Title,
        //    //}).ToList();
        //    return View(blogdetay);
        //}

    }
}