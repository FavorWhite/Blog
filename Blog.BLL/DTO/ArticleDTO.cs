using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ArticleText { get; set; }

        public int CategoryId { get; set; }

        public string ApplicationUserId { get; set; }
        public DateTime ArticleCreationTime { get; set; }

        public ICollection<TagDTO> ArticleTags { get; set; }
        public ArticleDTO()
        {
            ArticleTags = new List<TagDTO>();
        }
    }
}
