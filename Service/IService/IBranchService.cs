using Core.Data.DTO;
using Core.Utils;

namespace Service.IService
{
    public interface IBranchService
    {
        public string UserId { get; set; }
        ResultModel Get(string? InvoiceNo, string? BatchNo, string? ShipTo, DateTime? FromDate=null, DateTime? ToDate=null);
        //Task<ResultModel> GetType(int pageIndex, int pageSize, string? Search);
        //Task<int> GetCount();
        ////Task<ResultModel> Get(int pageIndex, int pageSize, FilterVM? model);
        //Task<ResultModel> Get(int pageIndex, int pageSize, string? Search);
        //ResultModel Get(int id);
        //Task<ResultModel> CreateOrUpdate(BranchVM model);
        //Task<ResultModel> Delete(int id);
        //Task<ResultModel> Export(string? Type, FilterVM? model);
    }
}
