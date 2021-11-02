using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TelecomProject.Data;

namespace TelecomProject.API.Handlers
{
    public class JwtAuthHandler : IJwtAuthHandler
    {
        private readonly TelecomProjectContext _context;
        private readonly string key;
        public JwtAuthHandler(string key, TelecomProjectContext context)
        {
            _context = context;
            this.key = key;
        }
        public string Authenticate(string username, string password)
        {
            var person = _context.People.Include(p => p.Login).FirstOrDefaultAsync(p => p.Login.Username == username && p.Login.Password == password);
            
            if (person == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
