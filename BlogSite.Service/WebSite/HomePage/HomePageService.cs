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
        public BlogModel BlogDetail(string link)
        {
            var blog = _context.Blogs.Where(x => x.Link == link);
            var blogdetay = _context.Blogs.Where(x => x.Link == link).FirstOrDefault();
            blogdetay.BlogClick += 1;
            _context.SaveChanges();
            return (from x in _context.Blogs
                    where x.Link == link
                    select new BlogModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        ImageUrl = x.ImageUrl,
                        Title = x.Title,
                        BlogClick = (int)blogdetay.BlogClick,
                        Date = x.Date,
                        Link = x.Link
                    }).FirstOrDefault();
        }

        public List<BlogModel> RandomBlogs()
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
                                  FROM [dbo].[Blogs] ORDER BY NEWID()";
                var randomBlogs = (db.Query<BlogModel>(_sql)).ToList();
                return randomBlogs;
            }
        }
        public List<BlogModel> LastBlogs()
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
                                   ,[Link]
                                    FROM [dbo].[Blogs] B 
	                                INNER JOIN [dbo].[Category] bc ON B.CategoryId=bc.CategoryId ORDER BY Id desc";
                var lastBlogs = (db.Query<BlogModel>(_sql)).ToList();
                return lastBlogs;
            }
        }
        public List<BlogModel> PopularBlogs()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["BlogSiteConnectionString"].ConnectionString))
            {
                string _sql = @"    SELECT TOP 6 [Id]
                                   ,[Title]
                                   ,[Description]
                                   ,[ImageUrl]
                                   ,[BlogUrl]
                                   ,[Date]
	                               ,b.CategoryId
	                               ,bc.CategoryName
                                   ,[BlogClick]
                                    FROM [dbo].[Blogs] B 
	                                INNER JOIN [dbo].[Category] bc ON B.CategoryId=bc.CategoryId  order by b.BlogClick desc";
                var popularBlogs = (db.Query<BlogModel>(_sql)).ToList();
                return popularBlogs;
            }
        }
        public List<BlogModel> TechnologyBlogs()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["BlogSiteConnectionString"].ConnectionString))
            {
                string _sql = @"    SELECT  TOP 8 [Id]
                                   ,[Title]
                                   ,[Description]
                                   ,[ImageUrl]
                                   ,[BlogUrl]
                                   ,[Date]
	                               ,b.CategoryId
	                               ,bc.CategoryName
                                   ,[BlogClick]
                                    FROM [dbo].[Blogs] B 
	                                INNER JOIN [dbo].[Category] bc ON B.CategoryId=bc.CategoryId where b.CategoryId=1 order by NEWID()";
                var technologyBlogs = (db.Query<BlogModel>(_sql)).ToList();
                return technologyBlogs;
            }
        }
    }
}
