using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Models
{
    public class AuthorizeOptions
    {
        public const string ISSUER = "SchoolApi"; 
        public const string AUDIENCE = "http://localhost:50885/"; 
        const string KEY = "School_Api_Security";  
        public const int LIFETIME = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
