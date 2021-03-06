﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public virtual User User { get; set; }
    }
}
