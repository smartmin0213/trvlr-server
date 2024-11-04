using DevExpress.Xpo;
using System;

namespace TRVLR.Models
{
    public class User : XPObject
    {
        public User(Session session) : base(session) { }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Hashed password
        public bool IsVerified { get; set; } = false; // Verification status
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}