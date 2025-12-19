using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Server.Domain
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
