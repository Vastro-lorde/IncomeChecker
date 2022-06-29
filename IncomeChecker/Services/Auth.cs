using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IncomeChecker.Services
{
    public class Auth : IAuth
    {
        private readonly IConfiguration _configuration;
        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken()
        {
            List<Claim> claims = new List<Claim>
            {
                // Creates the Claim from claim key in the Token section of AppSettings.
                new Claim(ClaimTypes.Name, _configuration.GetSection("Token:Claim").Value)
            };

            // Creates the key from the secret in AppSettings.
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Token:Secret").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                audience: _configuration.GetSection("Token:Audience").Value,
                issuer: _configuration.GetSection("Token:Issuer").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            var jsonToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jsonToken;
        }
    }
}
