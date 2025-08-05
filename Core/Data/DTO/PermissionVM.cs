namespace Core.Data.DTO
{
    public class PermissionVM
    {
        public int Id { get; set; } = 0;
        public int ControlId { get; set; }
        public string RoleId { get; set; }
        public string Route { get; set; }
        public bool IsActive { get; set; }
    }
}
