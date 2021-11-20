using Blog.Data;
using BlogSite.ViewModels;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace BlogSite.Service.WebSite.Category
{
    public class CategoryBlogsService
    {
        public List<BlogModel> CategoryBlogList(int categoryId)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["BlogSiteConnectionString"].ConnectionString))
            {
                string _sql = @"    SELECT TOP (10) [Id]
                                    ,[Title]
                                    ,[Description]
                                    ,[ImageUrl]
                                    ,[BlogUrl]
                                    ,[Date]
                                    ,[BlogClick]
									,c.CategoryName
									,c.CategoryId
                                    ,[Link]
                                    FROM [Blog].[dbo].[Blogs] b
									INNER JOIN Category c ON b.CategoryId=c.CategoryId where b.CategoryId=@categoryId";
                var categoryBlogs = (db.Query<BlogModel>(_sql, new { categoryId = categoryId })).Take(10).ToList();
                return categoryBlogs;
            }
        }

    }
}
