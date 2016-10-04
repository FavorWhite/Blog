﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public Tag()
        {
            ApplicationUsers = new List<ApplicationUser>();
            Articles = new List<Article>();
        }
    }
}
