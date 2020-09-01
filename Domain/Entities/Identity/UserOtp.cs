using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class UserOtp
    {
        public int Id { get; set; }
        public string Otp { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int UserId { get; set; }
        public virtual User User{ get; set; }

        public bool IsActive { get; set; }
    }
}
