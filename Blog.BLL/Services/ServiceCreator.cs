using Blog.BLL.Interfaces;
using Blog.DAL.Repositories;


namespace Blog.BLL.Services
{
    public class ServiceCreator  : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {

            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}
