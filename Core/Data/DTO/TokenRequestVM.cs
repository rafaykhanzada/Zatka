using System.ComponentModel.DataAnnotations;

namespace Core.Data.DTO
{
    public class TokenRequestVM
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
