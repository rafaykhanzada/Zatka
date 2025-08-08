using Core.Data.DataContext;
using Core.Data.Entities;
using Repository.IRepository;

namespace Repository.Repository
{
    public class RTRepository : RepositoryBase<RefreshToken>, IRTRepository
    {
        public RTRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
