using Blog.Data;
using BlogSite.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BlogEntities db = new BlogEntities();

        public ActionResult Index()
        {
            var blog = db.Blogs.Select(x => new BlogModel()
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                Date = x.Date
            }).ToList();
            return View(blog);
        }
        public ActionResult BlogDetay(int id)
        {
            var blog = db.Blogs.Where(x => x.Id == id);
            var blogdetay = db.Blogs.Where(x => x.Id == id).FirstOrDefault();
            //var blogdetay = blog.Select(x => new BlogModel()
            //{
            //    Id=x.Id,
            //    Description = x.Description,
            //    ImageUrl = x.ImageUrl,
            //    Title = x.Title,
            //}).ToList();
            return View(blogdetay);
        }

    }
}