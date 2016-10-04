using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string CommentText { get; set; }
        public DateTime CommentCreationTime { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
