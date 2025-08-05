using Core.Data.Common;

namespace Core.Data.Entities
{
    public class ActivityLog : HasIdDate
    {
        public string? Module { get; set; }
        public string? Action { get; set; }
        public string? Path { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? RequestedOn { get; set; }
    }
}
