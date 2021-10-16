using Blog.Data;
using BlogSite.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<List<BlogModel>> GetProducts(BlogModel model)
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
    }
}
