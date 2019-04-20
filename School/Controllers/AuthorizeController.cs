using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using School.Models;
using School.Models.DBManager;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        [HttpPost("token")]
        public ActionResult Token()
        {
            var username = Request.Headers["username"];
            var password = Request.Headers["password"];

            var identity = GetIdentity(username, password);

            if (identity == null)
            {
                return NotFound();
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthorizeOptions.ISSUER,
                    audience: AuthorizeOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthorizeOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthorizeOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            Response.ContentType = "application/json";

            return Ok(encodedJwt);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Person person = db.Persons.FirstOrDefault(p => p.Login == username && p.Password == password);

                if (person != null )
                {
                    if (person.Password == password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                        };

                        ClaimsIdentity claimsIdentity =
                        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                            ClaimsIdentity.DefaultRoleClaimType);
                        return claimsIdentity;
                    }
                }
                else
                {
                    school schools = db.Schools.FirstOrDefault(s => s.schoolId == 1);
                    if(schools==null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, "ADMIN@gmail.com"),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin")
                        };

                        ClaimsIdentity claimsIdentity =
                            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                        return claimsIdentity;
                    }
                }
            }
            return null;
        }
    }
}
