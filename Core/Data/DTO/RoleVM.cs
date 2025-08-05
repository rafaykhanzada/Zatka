using System.ComponentModel.DataAnnotations;

namespace Core.Data.DTO
{
    public class RoleVM
    {
        public string Name { get; set; }
        public string? Id { get; set; }
        public bool IsActive { get; set; }

    }
}
