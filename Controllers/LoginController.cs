using APISED.DTO;
using APISED.Modelo;
using APISED.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APISED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginController> log;
        private readonly IAPIUsuariosSQLServer repositorio;
        public LoginController(IConfiguration configuration, ILogger<LoginController> l, IAPIUsuariosSQLServer r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<APIUsuarioDTO>> Login(LoginAPI usuarioLogin)
        {
            UsuarioAPI Usuario = null;
            Usuario = await AutenticarUsuarioAsync(usuarioLogin);
            if (Usuario == null)
                throw new Exception("Credenciales no válidas");
            else
                Usuario = GenerarTokenJWT(Usuario);

            return Usuario.convertirDTO();
        }
        private async Task<UsuarioAPI> AutenticarUsuarioAsync(LoginAPI usuarioLogin)
        {
            UsuarioAPI usuarioAPI = await repositorio.DameUsuarioAPI(usuarioLogin);
            return usuarioAPI;

        }

        private UsuarioAPI GenerarTokenJWT(UsuarioAPI usuarioInfo)
        {
            // Cabecera
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // Claims
            var _Claims = new[] {
                new Claim("usuario", usuarioInfo.Usuario),
                new Claim("emaiil", usuarioInfo.Email),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
            };

            //Payload
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                     expires: DateTime.UtcNow.AddHours(24)
                );

            // Token
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );
            usuarioInfo.Token = new JwtSecurityTokenHandler().WriteToken(_Token);

            return usuarioInfo;
        }
    }
}
