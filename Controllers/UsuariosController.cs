using Microsoft.AspNetCore.Mvc;
using APINotificacionesV2.Models.Entities;
using APINotificacionesV2.Models.Repository.IRepository;
using Microsoft.Extensions.Options;
using APINotificacionesV2.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace APINotificacionesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IRepository<Usuarios> _UsuariosRepository;
        private readonly IConfiguration _config;
        public UsuariosController(IRepository<Usuarios> UsuariosRepository, IConfiguration config, IOptions<DatabaseSettings> databaseSettings)
        {
            _UsuariosRepository = UsuariosRepository; 
            _config = config;
        }
         

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUsuarios()
        {
            var usuarios = await _UsuariosRepository.GetAll();
            return Ok(usuarios);
        }

    
        [HttpGet("{nombre}")]
        [Authorize]
        public async Task<ActionResult>  GetUsuarios([FromQuery] int UsuarioId)
        {
            var usuario = await _UsuariosRepository.GetById(UsuarioId);
            return Ok(usuario);
        }

        

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Usuarios>> LoginUsuarios(Usuarios usuario)
        {
            var registrado = await _UsuariosRepository.UsuarioExists(usuario);
            if (registrado is null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Message = "Usuario debe registrarse" });
            }

            var token = Authentication.Authentication.GenerateToken(usuario, _config).Result;
            return Ok(new { Token = token, Nombre = usuario.NombreUsuario, Email = usuario.NombreUsuario });

        }

        [HttpPost]
        [Route("Registro")]
        public async Task<ActionResult<Usuarios>> RegistroUsuarios(Usuarios usuario)
        {
            var usuariocreado = await _UsuariosRepository.Create(usuario);
            if (usuariocreado is not null)
            {
                return StatusCode(StatusCodes.Status200OK, new { Message = "Usuario creado con exito" });
            }

            return StatusCode(500, new { Message = "No se logro crear el usuario" });

        }

    }


}
