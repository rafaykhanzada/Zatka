using Core.Data.DataContext;
using Repository.IRepository;
using Repository.Repository;

namespace UnitofWork
{
    public class UnitOfWork(ApplicationDbContext _db) : IUnitOfWork, IDisposable
    {
        private IBranchRepository _branchRepository;
        public IBranchRepository BranchRepository => _branchRepository ??= new BranchRepository(_db);
        public async Task<int> CompleteAsync() => await _db.SaveChangesAsync();
        public int Complete() => _db.SaveChanges();
        public void Rollback()
        {
            _db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
