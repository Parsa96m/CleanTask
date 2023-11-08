using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Entitties
{
    public class userapp : IdentityUser
    {
        public string firstname { get; set; }
    }
}
