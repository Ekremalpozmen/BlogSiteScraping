using Blog.Data;
using BlogSite.LightInject;
using BlogSite.LightInject.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSite
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var container = new LightInject.ServiceContainer();
            container.RegisterControllers();
            container.Register(typeof(BlogEntities), new PerRequestLifeTime());
            container.Register<BlogSiteService.Infrastructure.ICacheService, BlogSiteService.Infrastructure.Web.InMemoryCache>(new PerRequestLifeTime());
            System.Net.ServicePointManager.SecurityProtocol |=
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            container.Register(typeof(BlogSite.Services.WebSite.HomePage.HomePageService), new PerRequestLifeTime());
            container.EnableMvc();
        }
    }
}

