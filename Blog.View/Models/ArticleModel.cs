using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.View.Models
{
    public class ArticleModel
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
        public string CommentText { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string ApplicationUserId { get; set; }
        public IEnumerable<int> TagsIds { get; set; }

        public DateTime ArticleCreationTime { get; set; }
    }
}