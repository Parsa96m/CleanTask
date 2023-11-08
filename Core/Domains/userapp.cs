using Microsoft.AspNetCore.Identity;

namespace CleanTask.Domains
{
    public class userapp : IdentityUser
    {
        public string firstname { get; set; }
    }
}
