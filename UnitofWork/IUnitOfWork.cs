using Repository.IRepository;

namespace UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBranchRepository BranchRepository { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
