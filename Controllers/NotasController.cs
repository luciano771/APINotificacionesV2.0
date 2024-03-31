using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Authorization;
using APINotificacionesV2.Models.Entities;
using APINotificacionesV2.Models.Repository.IRepository;
 

namespace APINotificacionesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {
        private readonly IRepository<Notas> _NotasRepository;

        public NotasController(IRepository<Notas> NotasRepository)
        {
            _NotasRepository = NotasRepository;
        }
         

        [HttpPost]
        [Authorize]
        [Route("Notas")]
        public async Task<ActionResult<Notas>> AgregarNotas(Notas nota)
        {
            var notas= await _NotasRepository.Create(nota);
            if(notas is not null)
            {
                return StatusCode(200, "Nota creada con exito");
            }
            return StatusCode(500, "No se logro crear la nota");

        }

      
        [HttpGet]
        [Authorize]
        [Route("ListarNotasId")]
        public async Task<ActionResult> GetNotasId([FromQuery] int IdUsuario, [FromQuery] int notaId)
        {
            var NotasUsuarioId =  await _NotasRepository.GetById(notaId, IdUsuario);

            if (NotasUsuarioId == null)
            {
                return NotFound();
            }
            return Ok(NotasUsuarioId);
        }


        [HttpDelete]
        [Authorize]
        [Route("BorrarNota")]
        public async Task<ActionResult<Notas>> BorrarNota([FromQuery] int idnotas, [FromQuery] int UsuarioId)
        {
            var NotasUsuario = await _NotasRepository.Delete(UsuarioId, idnotas);

            if (!NotasUsuario) { return StatusCode(200, "Se elimino correctamente"); }
             
            return StatusCode(500,"No se logro eliminar correctamente");

        }
         

    }


}
