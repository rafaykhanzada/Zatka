using Repository.IRepository;

namespace UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IBranchRepository BranchRepository { get; }
        IRTRepository RefreshTokenRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
