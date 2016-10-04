using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<UserDTO> Users { get; set; }
        public CategoryDTO()
        {
            Users = new List<UserDTO>();
        }
    }
}
