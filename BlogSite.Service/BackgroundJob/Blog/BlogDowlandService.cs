using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Blog.Data;
using Hangfire;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace BlogSite.Services.BackgroundJob.Blog
{
    public class BlogDowlandService
    {
        [AutomaticRetry(Attempts = 0)]
        public void BlogJobService()
        {
            using (BlogEntities db = new BlogEntities())
            {
                var bloggers = db.Bloggers.ToList();
                foreach (var item in bloggers)
                {
                    GetProductForUrl(item.BloggersUrl);
                }
            }
        }
        public void GetProductForUrl(string bloggersUrl)
        {
            if (bloggersUrl.Contains("www.aorhan.com"))
            {
                AOrhan(bloggersUrl);
            }
            if (bloggersUrl.Contains("www.hduman.com"))
            {
                HDuman(bloggersUrl);
            }
        }

        private static void AOrhan(string bloggersUrl)
        {

            Uri url = new Uri(bloggersUrl);
            WebClient client = new WebClient();
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
            client.Headers.Add(
                "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            client.Encoding = Encoding.UTF8;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string html = client.DownloadString(url);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var blogList = document.DocumentNode.Descendants().Where(x => x.HasClass("jeg_postblock_5")).FirstOrDefault();
            var blogUrlList = blogList.Descendants().Where(x => x.HasClass("jeg_thumb")).ToList();

            foreach (var item in blogUrlList)
            {
                var blogImage = item.SelectSingleNode(".//img").Attributes["data-src"].Value;
                var blogUrl = item.SelectSingleNode(".//a[@href]").Attributes["href"].Value;
                Uri urls = new Uri(blogUrl);
                WebClient clients = new WebClient();
                clients.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
                clients.Headers.Add(
                    "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                clients.Encoding = Encoding.UTF8;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string htmls = clients.DownloadString(urls);
                HtmlDocument documents = new HtmlDocument();
                documents.LoadHtml(htmls);

                using (BlogEntities db = new BlogEntities())
                {
                    var blogTitle = documents.DocumentNode.Descendants()?.FirstOrDefault(n => n.HasClass("jeg_post_title"))?.InnerHtml;
                    var blogDescription = documents.DocumentNode.Descendants()?.Where(n => n.HasClass("entry-content")).FirstOrDefault()?.OuterHtml.Replace("data-src", "src"); ;

                    var blog = db.Blogs.FirstOrDefault(x => x.BlogUrl == blogUrl);
                    if (blog == null)
                    {
                        blog = new Blogs
                        {
                            Title = blogTitle,
                            Description = blogDescription,
                            ImageUrl = blogImage,
                            BlogUrl = blogUrl,
                            Date = DateTime.Now,
                        };
                        db.Blogs.Add(blog);
                        db.SaveChanges();
                    }
                }
            }
        }

        private static void HDuman(string bloggersUrl)
        {
            Uri url = new Uri(bloggersUrl);
            WebClient client = new WebClient();
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
            client.Headers.Add(
                "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            client.Encoding = Encoding.UTF8;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string html = client.DownloadString(url);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var blogList = document.DocumentNode.Descendants().Where(x => x.HasClass("advancedPostsWidget1")).FirstOrDefault();
            var blogUrlList = blogList.Descendants().Where(x => x.HasClass("post-container")).ToList();

            foreach (var item in blogUrlList)
            {
                var blogImage = item.SelectSingleNode(".//img").Attributes["data-src"].Value;
                var blogUrl = item.SelectSingleNode(".//a[@href]").Attributes["href"].Value;
                Uri urls = new Uri(blogUrl);
                WebClient clients = new WebClient();
                clients.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
                clients.Headers.Add(
                    "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                clients.Encoding = Encoding.UTF8;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string htmls = clients.DownloadString(urls);
                HtmlDocument documents = new HtmlDocument();
                documents.LoadHtml(htmls);

                using (BlogEntities db = new BlogEntities())
                {
                    var blogTitle = documents.DocumentNode.Descendants()?.FirstOrDefault(n => n.HasClass("singleHeading")).SelectSingleNode(".//h1").InnerHtml;
                    var blogDescription = documents.DocumentNode.Descendants()?.Where(n => n.Id == "singleContent").FirstOrDefault().OuterHtml;

                    var blog = db.Blogs.FirstOrDefault(x => x.BlogUrl == blogUrl);
                    if (blog == null)
                    {
                        blog = new Blogs
                        {
                            Title = blogTitle,
                            Description = blogDescription,
                            ImageUrl = blogImage,
                            BlogUrl = blogUrl,
                            Date = DateTime.Now,
                        };
                        db.Blogs.Add(blog);
                        db.SaveChanges();
                    }
                }
            }

        }
    }
}