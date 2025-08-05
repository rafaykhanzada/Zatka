using Core.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data.Entities
{
    public class Permission : HasIdDate
    {
        [ForeignKey("Control")]
        public int? ControlId { get; set; }
        [ForeignKey("Role")]
        public string? RoleId { get; set; }
        public string? Route { get; set; }
        public virtual Role? Role { get; set; } = null!;
    }
}
