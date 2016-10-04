using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.View.Models
{
    public class AddCommentModel
    {
        [Required]
        public string CommentText { get; set; }
        public int ArticleID { get; set; }
    }
}