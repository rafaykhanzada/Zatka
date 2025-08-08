using System.ComponentModel.DataAnnotations;

namespace Core.Data.DTO
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Email|UserName")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
