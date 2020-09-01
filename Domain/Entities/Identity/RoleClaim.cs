using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual Role Role { get; set; }
    }
}
