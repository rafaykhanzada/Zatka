using Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class Branch:HasId
    {
        public string? BranchCode { get; set; }
        public string? branch { get; set; }
        public string? SalesBranch { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public int? RegionId { get; set; }
        public int? UserId { get; set; }
        public int? DealerId { get; set; }
    }
}
