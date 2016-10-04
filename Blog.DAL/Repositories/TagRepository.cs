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
    class TagRepository : IRepository<Tag>
    {
        private ApplicationContext db;

        public TagRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return db.Tags;
        }

        public Tag Get(int id)
        {
            return db.Tags.Find(id);
        }

        public void Create(Tag item)
        {
            db.Tags.Add(item);
        }

        public void Update(Tag item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Tag item = db.Tags.Find(id);
            if (item != null)
                db.Tags.Remove(item);
        }
    }
}
