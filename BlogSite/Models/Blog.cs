using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Models
{
    public class BlogModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? Date { get; internal set; }
    }
}