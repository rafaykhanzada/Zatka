namespace Core.Data.DTO
{
    public class UserVM
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Designation { get; set; } = "Admin";
        public string? UserType { get; set; } = "BackOffice";
        public string? AuthType { get; set; }
        public string? Token { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public string? FcmToken { get; set; }
        public string? PhoneNumber { get; set; }

        public IList<string>? Roles { get; set; }
    }
}
