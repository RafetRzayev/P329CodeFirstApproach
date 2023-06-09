using Microsoft.AspNetCore.Identity;

namespace P329CodeFirstApproach.DataAccessLayer
{
    public class User : IdentityUser
    {
        public string? Fullname { get; set; }
    }
}
