using Microsoft.AspNetCore.Identity;

namespace ProductCatalog.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
