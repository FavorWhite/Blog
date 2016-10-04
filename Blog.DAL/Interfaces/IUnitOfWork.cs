using Blog.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Entities;

namespace Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IClientManager ClientManager { get; }
        IRepository<Article> Articles { get; }
        IRepository<Category> Categories { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Tag> Tags { get; }

        Task SaveAsync();
        void Save();
    }
}
