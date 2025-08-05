using Core.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using System.Drawing.Printing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDP.Controllers
{
    [Route("api/getinvoice")]
    [ApiController]
    public class HomeController(IBranchService branchService) : ControllerBase
    {
        private readonly IBranchService _branchService = branchService;

        // GET api/<LookupController>/5
        [HttpGet]
        public IActionResult Get(string? InvoiceNo=null, string? BatchNo=null, string? ShipTo=null, DateTime? FromDate=null, DateTime? ToDate=null) => Ok(_branchService.Get(InvoiceNo, BatchNo, ShipTo, FromDate, ToDate));

    }
}
