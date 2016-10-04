using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.View.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public IEnumerable<string> ApplicationUserIds { get; set; }

    }
}