using APINotificacionesV2.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace APINotificacionesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Settings : ControllerBase
    {
        private readonly IOptions<DatabaseSettings> _databaseSettings;
        public Settings(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        [HttpGet]
        [Route("DatabaseConexion")]
        public ActionResult DatabaseConexionKMS()
        {
            var settings = _databaseSettings.Value;
            return Ok(settings);
        }

        [HttpGet]
        [Route("FirebaseKMS")]
        public ActionResult FirebaseKMS()
        {
            var settings = APINotificacionesV2.ExternalServices.KMSFunctions.getSecretFirebase().Result;
            return Ok(settings);
        }

        [HttpGet]
        [Route("BearerKMS")]
        public ActionResult BearerKMS()
        {
            var settings = APINotificacionesV2.ExternalServices.KMSFunctions.GetSecretToken().Result;
            return Ok(settings);
        }
    }
}