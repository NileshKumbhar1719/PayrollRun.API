using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Auth.Data
{
    public class UserRegister :IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        
    }
}
