using Blog.Data;
using BlogSite.ViewModels;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Services.WebSite.HomePage
{
    public class HomePageService
    {
        private readonly BlogEntities _context;

        public HomePageService(BlogEntities context)
        {
            _context = context;
        }

        public async Task<List<BlogModel>> GetBlogs(BlogModel model)
        {
            return await (from x in _context.Blogs.OrderByDescending(a => a.Id)
                          select new BlogModel()
                          {
                              Id = x.Id,
                              Description = x.Description,
                              ImageUrl = x.ImageUrl,
                              Title = x.Title,
                              Date = x.Date
                          }).Take(6).ToListAsync().ConfigureAwait(false);
        }
        public async Task<List<BlogModel>> BlogDetail(BlogModel model, int id)
        {
            var blog = _context.Blogs.Where(x => x.Id == id);
            var blogdetay = _context.Blogs.Where(x => x.Id == id).FirstOrDefault();
            blogdetay.BlogClick = blogdetay.BlogClick + 1;
            _context.SaveChanges();
            return await (blog.Select(x => new BlogModel()
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
                BlogClick = (int)blogdetay.BlogClick,
                Date = x.Date
            }).ToListAsync().ConfigureAwait(false));
        }

        public List<BlogModel> RandomBlogs(BlogModel model)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["BlogSiteConnectionString"].ConnectionString))
            {
                string _sql = @"  SELECT TOP 6 [Id]
                                  ,[Title]
                                  ,[Description]
                                  ,[ImageUrl]
                                  ,[BlogUrl]
                                  ,[Date]
                                  ,[CategoryId]
                                  ,[BlogClick]
                                  FROM [Blog].[dbo].[Blogs] ORDER BY NEWID()";
                var randomBlogs = (db.Query<BlogModel>(_sql)).ToList();
                return randomBlogs;
            }
        }
        public List<BlogModel> LastBlogs(BlogModel model)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["BlogSiteConnectionString"].ConnectionString))
            {
                string _sql = @"   SELECT TOP 13 [Id]
                                   ,[Title]
                                   ,[Description]
                                   ,[ImageUrl]
                                   ,[BlogUrl]
                                   ,[Date]
	                               ,b.CategoryId
	                               ,bc.CategoryName
                                   ,[BlogClick]
                                    FROM [Blog].[dbo].[Blogs] B 
	                               INNER JOIN [Blog].[dbo].[Category] bc ON B.CategoryId=bc.CategoryId ORDER BY Id desc";
                var popularBlogs = (db.Query<BlogModel>(_sql)).ToList();
                return popularBlogs;
            }
        }

    }
}
