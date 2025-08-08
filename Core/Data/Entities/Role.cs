using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Core.Data.Entities
{
    public class Role:IdentityRole
    {
       
        public bool IsActive { get; set; }
    }
}
