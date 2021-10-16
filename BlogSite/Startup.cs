using BlogSite.Services.BackgroundJob.Blog;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using ProjeService.Filters;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;

[assembly: OwinStartup(typeof(BlogSite.Startup))]

namespace BlogSite
{
    public class Startup
    {
       public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseRecommendedSerializerSettings()
                            .UseSqlServerStorage(ConfigurationManager.ConnectionStrings["BlogSiteConnectionString"].ConnectionString, new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            });
            RecurringJob.AddOrUpdate<BlogDowlandService>(x => x.BlogJobService(), "13 0,8,16 * * *");
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                AppPath = VirtualPathUtility.ToAbsolute("~"),
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = (int)(Environment.ProcessorCount),
                StopTimeout = TimeSpan.FromSeconds(10),
            });
        }
    }
} 