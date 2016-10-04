using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.DAL.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public ApplicationUser()
        {
            Categories = new List<Category>();
            Articles = new List<Article>();
            Comments = new List<Comment>();
            Tags = new List<Tag>();
        }
        

    }
}
