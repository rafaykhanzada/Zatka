using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.DTO
{
    public class BranchVM
    {
        public int Id { get; set; }
        public string? BranchCode { get; set; }
        public string? Branch { get; set; }
        public string? SalesBranch { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? RegionId { get; set; }
        //public int? UserId { get; set; }
        //public int? DealerId { get; set; }
    }
}
