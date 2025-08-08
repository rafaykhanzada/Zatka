using Core.Data.DataContext;
using Core.Data.Entities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
