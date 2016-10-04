using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.View.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> ApplicationUserIds { get; set; }
        public List<int> ArticleIds { get; set; }
    }
}