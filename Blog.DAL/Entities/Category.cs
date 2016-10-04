using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public Category()
        {
            ApplicationUsers = new List<ApplicationUser>();
            Articles = new List<Article>();
        }
    }
}
