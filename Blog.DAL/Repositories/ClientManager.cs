using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;

namespace Blog.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public ClientProfile Find(string id)
        {
            return Database.ClientProfiles.Find(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
