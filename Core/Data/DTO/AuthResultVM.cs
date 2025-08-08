using System.ComponentModel.DataAnnotations;

namespace Core.Data.DTO
{
    public class AuthResultVM
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpireAct { get; set; }
    }
    public class AuthPasswordVM
    {
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string OldPassword { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!;
    }
}
