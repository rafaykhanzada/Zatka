using Core.Paging;
using System.Linq.Expressions;

namespace Repository.IRepository
{
    public interface IRepositoryBase<T> where T : class
    {
        int GetCount();
        Task<int> GetCountAsync();
        Task<IEnumerable<T>> GetAllPagedAsync(int limit, int offset);
        Task<IEnumerable<T>> GetAllPagedAsync(Expression<Func<T, bool>> condition,int limit, int offset);
        IEnumerable<T> GetAllPaged(int limit, int offset);
        T SoftDelete(T entity,string UserId);
        T Insert(T model);
        T Update(T model);
        void Delete(T model);
        IEnumerable<T> GetAll();
        T GetById(int? id);
        T GetById(Guid? id);
        IEnumerable<T> Get(Expression<Func<T, bool>> condition);
        IEnumerable<T> Get(Expression<Func<T, bool>> condition, int pageIndex, int pageSize);
        IEnumerable<T> GetAll(int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<T> GetAllIPagedList(int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertAll(IEnumerable<T> model);
        T GetFirst(Func<T, bool> condition);
        void UpdateAll(IEnumerable<T> model);
        void DeleteAll(IEnumerable<T> model);
        //Async Method 
        Task<T> AddAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<IList<T>> GetAllIncludeAsync(params Expression<Func<T, object>>[] navigationProperties);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> condition);
        Task<ICollection<T>> PaggedListAsync(int? pageSize, int? page, params Expression<Func<T, object>>[] navigationProperties);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetSingleIncludeAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
    }
}
