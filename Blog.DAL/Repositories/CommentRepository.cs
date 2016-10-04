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
    public class CommentRepository : IRepository<Comment>
    {
        private ApplicationContext db;

        public CommentRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Comment item = db.Comments.Find(id);
            if (item != null)
                db.Comments.Remove(item);
        }
    }
}
