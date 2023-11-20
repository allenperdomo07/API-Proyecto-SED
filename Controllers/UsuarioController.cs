using APISED.DTO;
using APISED.Modelo;
using APISED.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISED.Controllers
{
    [Route("usuarios")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosenmemoria repositorio;

        public UsuarioController(IUsuariosenmemoria r)
        {
            this.repositorio = r;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<UsuarioDTO> DameUsuarios()
        {
            var listaUsuarios = repositorio.DameUsuarios().Select(o=>o.convertirDTO());
            return listaUsuarios;
        }

        [HttpGet("{codUsuario}")]
        [Authorize]
        public ActionResult<UsuarioDTO> DameUsuario(string codUsuario)
        {
            var usuario = repositorio.DameUsuario(codUsuario).convertirDTO();

            if (usuario is null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<UsuarioDTO> CrearUsuario(UsuarioDTO o)
        {
            Usuario usuario = new Usuario
            {
                Nombre = o.Nombre,
                profesion = o.profesion,
                NumeroTel = o.NumeroTel,
                SKU = o.SKU,
               

            };

            repositorio.CrearUsuario(usuario);

            return usuario.convertirDTO();
        }

        [HttpPut]
        [Authorize]
        public ActionResult<UsuarioDTO> ModificarUsuario(String codUsuario, UsuarioActualizarDTO o)
        {
            Usuario existeUsuario = repositorio.DameUsuario(codUsuario);
            if (existeUsuario is null)
            {
                return NotFound();
            }

            existeUsuario.Nombre = o.Nombre;
            existeUsuario.profesion = o.profesion;
            
      

            repositorio.ModificarUsuario(existeUsuario);

            return existeUsuario.convertirDTO();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult BorrarUsuario(String codUsuario)
        {
            Usuario existeUsuario = repositorio.DameUsuario(codUsuario);
            if (existeUsuario is null)
            {
                return NotFound();
            }

            repositorio.BorrarUsuario(codUsuario);

            return NoContent();
        }



    }
}


