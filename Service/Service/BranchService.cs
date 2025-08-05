using AutoMapper;
using Core.Constant;
using Core.Data.DTO;
using Core.Data.Entities;
using Core.Paging;
using Core.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Service.IService;
using System.Linq.Expressions;
using UnitofWork;

namespace Service.Service
{
    public class BranchService(IUnitOfWork unitOfWork, ILogger<Branch> logger, IMapper mapper, ResultModel resultModel) : IBranchService
    {
        public string UserId { get; set; }
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<Branch> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly ResultModel _resultModel = resultModel;
       
        //public async Task<ResultModel> CreateOrUpdate(BranchVM model)
        //{
        //    try
        //    {
        //        var data = _mapper.Map<Branch>(model);
        //        var exist = _unitOfWork.BranchRepository.Get(x => x.Id == model.Id).FirstOrDefault();
              
        //        if (data.Id > 0)
        //        {
        //            if (exist == null)
        //            {
        //                _resultModel.Message = MessageString.NotFound;
        //                return _resultModel;
        //            }
        //            data.UpdatedAt = DateTime.Now;
        //            //data.UpdatedBy = UserId;
        //            //data.CreatedBy = exist.CreatedBy;
        //            data.CreatedAt = exist.CreatedAt;
        //            _unitOfWork.BranchRepository.Update(data);
        //            _resultModel.Message = MessageString.Updated;

        //        }
        //        else
        //        {
        //            data.CreatedAt = DateTime.Now;
        //            //data.UpdatedOn = DateTime.Now;
        //            //data.CreatedBy = UserId;
        //            _unitOfWork.BranchRepository.Insert(data);
        //            _resultModel.Message = MessageString.Created;

        //        }
        //        await _unitOfWork.CompleteAsync();
        //        _resultModel.Success = true;
        //        _resultModel.Data = _mapper.Map<BranchVM>(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Success = false;
        //        _resultModel.Message = MessageString.ServerError;
        //    }
        //    return _resultModel;
        //}

        //public async Task<ResultModel> Delete(int id)
        //{
        //    try
        //    {
        //        var data = _unitOfWork.BranchRepository.Get(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
        //        if (data != null)
        //        {
        //            _unitOfWork.BranchRepository.SoftDelete(data, UserId);
        //            await _unitOfWork.CompleteAsync();
        //            _resultModel.Message = MessageString.Deleted;
        //            _resultModel.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Success = false;
        //        _resultModel.Message = MessageString.ServerError;
        //    }
        //    return _resultModel;
        //}

        public ResultModel Get(string InvoiceNo,string BatchNo,string ShipTo,DateTime? FromDate=null,DateTime? ToDate=null)
        {
            try
            {

                string query = "EXEC [usp_APIGetinvoices]";
                List<string> parameters = new List<string>();

                if (!string.IsNullOrEmpty(InvoiceNo))
                    parameters.Add($"@InvoiceNo = '{InvoiceNo}'");

                if (!string.IsNullOrEmpty(BatchNo))
                    parameters.Add($"@BatchNo = '{BatchNo}'");

                if (!string.IsNullOrEmpty(ShipTo))
                    parameters.Add($"@ShipTo = '{ShipTo}'");

                if (FromDate != null)
                    parameters.Add($"@FromDate = '{FromDate:yyyy-MM-dd}'");

                if (ToDate != null)
                    parameters.Add($"@ToDate = '{ToDate:yyyy-MM-dd}'");

                if (parameters.Count > 0)
                    query += " " + string.Join(", ", parameters);


                var data = BaseUtil.RawSqlQuery<InvoiceVM>(query, x => new InvoiceVM
                {
                    InvoiceNo = (x[0] == DBNull.Value) ? "" : (string)x[0],
                    Manufacture = (x[1] == DBNull.Value) ? "" : (string)x[1],
                    Customer = (x[2] == DBNull.Value) ? "" : (string)x[2],
                    Country = (x[3] == DBNull.Value) ? "" : (string)x[3],
                    City = (x[4] == DBNull.Value) ? "" : (string)x[4],
                    TypeOfProduct = (x[5] == DBNull.Value) ? "" : (string)x[5],
                    ItemCode = (x[6] == DBNull.Value) ? "" : (string)x[6],
                    ItemDescription = (x[7] == DBNull.Value) ? "" : (string)x[7],
                    LotNumber = (x[8] == DBNull.Value) ? "" : (string)x[8],
                    ProductionDate = (x[9] == DBNull.Value) ? null : (DateTime)x[9],
                    ExpireDate = (x[10] == DBNull.Value) ? null : (DateTime)x[10],
                    LotQty = (x[11] == DBNull.Value) ? null : (decimal)x[11],
                    Qty = (x[12] == DBNull.Value) ? 0 : (decimal)x[12],
                    QtyUnit = (x[13] == DBNull.Value) ? "" : (string)x[13],
                    ShipToCustomer = (x[14] == DBNull.Value) ? "" : (string)x[14],
                    Truckinfo = (x[15] == DBNull.Value) ? "" : (string)x[15],

                });

                // Transform flat data into nested structure
                var transformedData = data
                    .GroupBy(x => new { x.InvoiceNo, x.Manufacture, x.Customer, x.Country, x.City, x.ShipToCustomer, x.Truckinfo })
                    .Select(invoiceGroup => new
                    {
                        InvoiceNo = invoiceGroup.Key.InvoiceNo,
                        Manufacture = invoiceGroup.Key.Manufacture,
                        Customer = invoiceGroup.Key.Customer,
                        Country = invoiceGroup.Key.Country,
                        City = invoiceGroup.Key.City,
                        ShipToCustomer = invoiceGroup.Key.ShipToCustomer,
                        Truckinfo = invoiceGroup.Key.Truckinfo,
                        Lines = invoiceGroup
                            .GroupBy(x => new { x.TypeOfProduct, x.ItemCode, x.ItemDescription, x.Qty, x.QtyUnit })
                            .Select(itemGroup => new
                            {
                                TypeOfProduct = itemGroup.Key.TypeOfProduct,
                                ItemCode = itemGroup.Key.ItemCode,
                                ItemDescription = itemGroup.Key.ItemDescription,
                                Qty = itemGroup.Key.Qty,
                                QtyUnit = itemGroup.Key.QtyUnit,
                                Lots = itemGroup
                                    .Select(lot => new
                                    {
                                        LotNumber = lot.LotNumber,
                                        ProductionDate = lot.ProductionDate,
                                        ExpireDate = lot.ExpireDate,
                                        LotQty = lot.LotQty
                                    })
                                    .ToList()
                            })
                            .ToList()
                    })
                    .ToList();
                _resultModel.Success = true;
                   _resultModel.Data = transformedData;
                if (data.Any())
                    _resultModel.Message = MessageString.Success;
                else
                    _resultModel.Message = MessageString.NotFound;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                _resultModel.Success = false;
                _resultModel.Data = new List<InvoiceVM>();
                _resultModel.Message = ex.Message.ToString();
            }
            return _resultModel;
        }
      

        //public async Task<ResultModel> Get(int pageIndex, int pageSize,FilterVM? model)
        //{
        //    try
        //    {
        //        #region Filter
        //        // Set Predicate
        //        Expression<Func<Branch, bool>> predicate = x => x.IsDeleted != true;
        //        if (!string.IsNullOrEmpty(model.SearchBy))
        //            predicate = BaseUtil.AddSearchCriteria<Branch>(model,predicate);
        //        #endregion
        //        var data = _unitOfWork.BranchRepository.Get(predicate, pageIndex,pageSize);
        //            _resultModel.Data = new ListModel<BranchVM>(_mapper.Map<IEnumerable<BranchVM>>(data),await _unitOfWork.BranchRepository.CountAsync(predicate));
        //            _resultModel.Success = true;
        //            _resultModel.Message = MessageString.Success;
        //            _logger.LogInformation(_resultModel.Message);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Success = false;
        //        _resultModel.Message = MessageString.ServerError;
        //    }
        //    return _resultModel;
        //}
        //public async Task<ResultModel> Get(int pageIndex, int pageSize,string? Search)
        //{
        //    try
        //    {
        //        #region Filter
        //        // Set Predicate
        //        Expression<Func<Branch, bool>> predicate = x => x.IsDeleted != true;
        //        if (!string.IsNullOrEmpty(Search))
        //            predicate = x=>!string.IsNullOrEmpty(x.branch) && x.branch.Contains(Search) || x.BranchCode.Contains(Search);
        //            //    predicate = BaseUtil.AddSearchCriteria<Branch>(model,predicate);
        //            #endregion
        //            var data = _unitOfWork.BranchRepository.Get(predicate);
        //            var pagedDate = new PagedList<Branch>(data.AsQueryable(),pageIndex,pageSize);
        //            _resultModel.Data = new ListModel<BranchVM>(_mapper.Map<IEnumerable<BranchVM>>(pagedDate),await _unitOfWork.BranchRepository.CountAsync(predicate));
        //            _resultModel.Success = true;
        //            _resultModel.Message = MessageString.Success;
        //            _logger.LogInformation(_resultModel.Message);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Success = false;
        //        _resultModel.Message = MessageString.ServerError;
        //    }
        //    return _resultModel;
        //}

        //public ResultModel Get(int id)
        //{
        //    try
        //    {
        //        var data = _unitOfWork.BranchRepository.Get(x => x.IsDeleted != true && x.Id == id).FirstOrDefault();
        //        if (data != null)
        //        {
        //            var result = _mapper.Map<BranchVM>(data);
        //            _resultModel.Success = true;
        //            _resultModel.Data = result;
        //        }
        //        else
        //        {
        //            _logger.LogInformation(MessageString.NotFound);
        //            _resultModel.Success = true;
        //            _resultModel.Message = MessageString.NotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Success = false;
        //        _resultModel.Message = MessageString.ServerError;
        //    }
        //    return _resultModel;
        //}
        //public async Task<int> GetCount()
        //{
        //    try
        //    {
        //        return await _unitOfWork.BranchRepository.GetCountAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //    }
        //    return 0;
        //}
        //public async Task<ResultModel> Export(string? Type, FilterVM? model)
        //{
        //    try
        //    {
        //        Expression<Func<Branch, bool>> predicate = x => x.IsDeleted != true;
        //        if (model != null && !string.IsNullOrEmpty(model.SearchBy))
        //            predicate = BaseUtil.AddSearchCriteria<Branch>(model,predicate);
        //        var data = _unitOfWork.BranchRepository.Get(predicate);

        //        if (!data.Any())
        //        {
        //            _logger.LogInformation("No Record Found!");
        //            _resultModel.Data = new ListModel<string>(list: new List<string>(), count: 0);
        //            _resultModel.Success = true;
        //            _resultModel.Message = "No Record Found!";
        //            return _resultModel;
        //        }
        //        var EmailVm = _mapper.Map<List<BranchVM>>(data);
        //        //await PopulateDealerNames(EmailVm);
        //        _resultModel.Success = true;
        //        _resultModel.Data = new ListModel<BranchVM>(EmailVm, data.Count());
        //        byte[] content = ExportUtility.ExportToExcel(EmailVm);
        //        _resultModel.Success = true;
        //        _resultModel.Data = content;
        //        _resultModel.Message = $"Total Items Exported {EmailVm.Count}";
        //        return _resultModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Success = false;
        //        _resultModel.Message = "Error While Get Record";
        //    }
        //    return _resultModel;
        //}
    }
}
