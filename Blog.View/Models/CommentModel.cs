using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.View.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required]
        public string CommentText { get; set; }
        public DateTime CommentCreationTime { get; set; }
        public string UserName { get; set; }
        public int ArticleId { get; set; }
    }
}