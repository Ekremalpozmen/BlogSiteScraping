using BlogSite.Controllers.Abstract;
using BlogSite.Service.WebSite.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    public class CategoryBlogsController : BaseController
    {
        private readonly CategoryBlogsService _categoryBlogsService;

        public CategoryBlogsController(CategoryBlogsService categoryBlogsService)
        {
            _categoryBlogsService = categoryBlogsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoryBlog
        public ActionResult CategoryBlogs(int id)
        {
            var result = _categoryBlogsService.CategoryBlogList(id);
            ViewBag.categoryName= result.Select(x=>x.CategoryName).FirstOrDefault();
            return View("Index", result);
        }
    }
}