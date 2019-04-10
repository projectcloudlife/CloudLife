using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private IConfiguration _conf;
        public TokenGeneratorService(IConfiguration conf)
        {
            _conf = conf;
        }
        public async Task<string> CreateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_conf["Key"]);
            var tokenInfo = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenData = tokenHandler.CreateToken(tokenInfo);
            var token = tokenHandler.WriteToken(tokenData);
            return token;
        }
    }
}
