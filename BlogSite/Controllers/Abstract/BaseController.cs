using Blog.Data;
using BlogSite.ViewModels.WebSite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers.Abstract
{
    public class BaseController : Controller
    {

        protected override void Initialize(System.Web.Routing.RequestContext context)
        {
            base.Initialize(context);
        }

        public CategoryModel Category { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext category)
        {

            using (BlogEntities db = new BlogEntities())
            {

                var categories = db.Category.ToList();

                foreach (var item in categories)
                {
                    Category = new CategoryModel()
                    {
                        Id = item.CategoryId,
                        CategoryName = item.CategoryName,
                    };
                }
            }

            base.OnActionExecuting(category);
        }

        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}