
using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using Blog.DAL.Identity;

namespace Blog.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;
        private ArticleRepository articleRepository;
        private CategoryRepository categoryRepository;
        private CommentRepository commentRepository;
        private TagRepository tagRepository;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
            articleRepository = new ArticleRepository(db);
            categoryRepository = new CategoryRepository(db);
            commentRepository = new CommentRepository(db);
            tagRepository = new TagRepository(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IRepository<Article> Articles
        {
            get
            {
                return articleRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return categoryRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return commentRepository;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                return tagRepository;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
