using System.Linq;
using Hangfire;


namespace BlogSite.Services.BackgroundJob.Product
{
    public class BlogJobService
    {
        [AutomaticRetry(Attempts = 0)]
        public void GetProductJobService()
        {
            using (BlogEntities db = new BlogEntities();)
            {
                
            }
        }
    }
}