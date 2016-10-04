using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentCreationTime { get; set; }
        public string ApplicationUserId { get; set; }
        public int ArticleId { get; set; }
    }
}
