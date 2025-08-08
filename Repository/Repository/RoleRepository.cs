using Core.Data.DataContext;
using Core.Data.Entities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
