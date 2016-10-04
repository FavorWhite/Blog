using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System.Data.Entity;

namespace Blog.DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationContext db;

        public CategoryRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories.Include(c => c.ApplicationUsers);
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Category item)
        {
            db.Categories.Add(item);
        }

        public void Update(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Category item = db.Categories.Find(id);
            if (item != null)
                db.Categories.Remove(item);
        }
    }
}
