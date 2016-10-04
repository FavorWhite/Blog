using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<UserDTO> Users { get; set; }
        public ICollection<ArticleDTO> TagArticles { get; set; }
        public TagDTO()
        {
            Users = new List<UserDTO>();
            TagArticles = new List<ArticleDTO>();
        }
    }
}
