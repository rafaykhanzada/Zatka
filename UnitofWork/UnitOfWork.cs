using Core.Data.DataContext;
using Repository.IRepository;
using Repository.Repository;

namespace UnitofWork
{
    public class UnitOfWork(ApplicationDbContext _db) : IUnitOfWork, IDisposable
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IRTRepository _rtRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_db);
        public IRTRepository RefreshTokenRepository => _rtRepository ??= new RTRepository(_db);
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_db);
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
