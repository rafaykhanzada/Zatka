using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Core.Data.Entities
{
    public class Role:IdentityRole
    {
        public Role()
        {
            Permission = new HashSet<Permission>();
        }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
