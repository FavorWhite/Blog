using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DAL.Entities
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string ShortDescription { get; set; }

        [Required]

        public string ArticleText { get; set; }

        public DateTime ArticleCreationTime { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Article()
        {
            Comments = new List<Comment>();
            Tags = new List<Tag>();
        }
    }
}
