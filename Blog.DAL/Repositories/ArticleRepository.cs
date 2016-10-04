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
    public class ArticleRepository : IRepository<Article>
    {
        private ApplicationContext db;

        public ArticleRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Article> GetAll()
        {
            return db.Articles;
        }

        public Article Get(int id)
        {
            return db.Articles.Find(id);
        }

        public void Create(Article item)
        {
            db.Articles.Add(item);
        }

        public void Update(Article item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Article item = db.Articles.Find(id);
            if (item != null)
                db.Articles.Remove(item);
        }
    }
}
