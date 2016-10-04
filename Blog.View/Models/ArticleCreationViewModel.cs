using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.View.Models
{
    public class ArticleCreationViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "The length should be between 3 and 256 characters")]
        public string Title { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "The length should be between 3 and 500 characters")]
        public string ShortDescription { get; set; }
        [Required]
        public string ArticleText { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Tags { get; set; }
    }
}