using Blog.Data;
using BlogSite.Controllers.Abstract;
using BlogSite.Services.WebSite.HomePage;
using BlogSite.ViewModels;
using BlogSite.ViewModels.WebSite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        private readonly HomePageService _homePageService;
        private readonly BlogEntities _context;

        public HomeController(HomePageService homePageService, BlogEntities context)
        {
            _homePageService = homePageService;
            _context = context;
        }

        public PartialViewResult CategoryList()
        {
            List<Category> categories = _context.Category.ToList();
            ViewBag.Categories = categories;
            return PartialView();

        }

        public async Task<ActionResult> Index(BlogModel model)
        {
            var result = await _homePageService.GetBlogs(model);
            return View(result);
        }

        public PartialViewResult RandomBlogs()
        {
            var result = _homePageService.RandomBlogs();
            return PartialView(result);
        }
        public PartialViewResult LastBlogs()
        {
            var result = _homePageService.LastBlogs();
            return PartialView(result);
        }
        public PartialViewResult PopularBlogs()
        {
            var result = _homePageService.PopularBlogs();
            return PartialView(result);
        }
        public PartialViewResult TechnologyBlogs()
        {
            var result = _homePageService.TechnologyBlogs();
            return PartialView(result);
        }

        public ActionResult BlogDetail(int id)
        {
            var result = _homePageService.BlogDetail(id);
            return View(result);
        }
    }
}