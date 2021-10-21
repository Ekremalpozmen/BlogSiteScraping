﻿using BlogSite.Services.WebSite.HomePage;
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

        public async Task<ActionResult> BlogDetail(BlogModel model, int id)
        {
            var result = await _homePageService.BlogDetail(model, id);
            return View(result.FirstOrDefault());
        }



    }
}