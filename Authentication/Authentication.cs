using APINotificacionesV2.ExternalServices;
using APINotificacionesV2.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APINotificacionesV2.Authentication
{
    public class Authentication
    { 
        public static async Task<string> GenerateToken(Usuarios users, IConfiguration _config)
        {
            var secret = await APINotificacionesV2.ExternalServices.KMSFunctions.GetSecretToken();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users.NombreUsuario),
                new Claim(ClaimTypes.Email, users.CorreoElectronico)
                // Agregar más claims según necesites
            };

            var token = new JwtSecurityToken(
            null,
            null,
            claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
           );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
 