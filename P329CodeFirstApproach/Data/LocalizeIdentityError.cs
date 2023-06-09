using Microsoft.AspNetCore.Identity;

namespace P329CodeFirstApproach.Data
{
    public class LocalizeIdentityError : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                 Description = "Email tekrarlana bilmez" ,
                 Code = "101"
            };
        }
    }
}
