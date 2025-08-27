using Core.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using System.Drawing.Printing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDP.Controllers
{
    //[Route("api/getinvoice")]
    [Route("api")]
    [ApiController]
    [Authorize]
    public class InvoiceController(IBranchService branchService) : ControllerBase
    {
        private readonly IBranchService _branchService = branchService;

        // GET api/<LookupController>/5
        [HttpGet]
        [Route("getinvoice")]
        public IActionResult Get(string? InvoiceNo=null, string? BatchNo=null, string? ShipTo=null, DateTime? FromDate=null, DateTime? ToDate=null) => Ok(_branchService.Get(InvoiceNo, BatchNo, ShipTo, FromDate, ToDate));
        
        [HttpGet]
        [Route("invoice-feedbacks")]
        public IActionResult GetFeedBacks(string? InvoiceNo=null) => Ok(_branchService.Get(InvoiceNo));
        
        [HttpPost]
        [Route("invoice-feedbacks")]
        public IActionResult Post([FromBody] List<InvoiceFeedbackVM> model) => Ok(_branchService.Post(model));

    }
}
