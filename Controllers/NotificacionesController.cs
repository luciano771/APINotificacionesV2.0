using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using APINotificacionesV2.Models.Entities;
using APINotificacionesV2.Services;
using static Google.Apis.Requests.BatchRequest;
using Microsoft.IdentityModel.Tokens;

namespace APINotificacionesV2.Controllers
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] Notificaciones notificaciones)
        {
            notificaciones.Token = "d7YIed0zTICd1IoOLXdR9F:APA91bF0n2z-1QV7rBKpUeXEbbKtX4MM_Cr7yqfwjuynQEBAHQhe84xsNg0RHRqoFPeU4MjDP7DgJWUvqXA7M3FAtm_MR3UgQ-RvhFJOrp6B6gRb4qVi3kTFVV3sZuXEf3sULxfpAnuZ";

            try
            {
                var response = await NotificationService.SendNotification(notificaciones);
                return StatusCode(200, $"Notificación enviada correctamente: {response}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }

        }
    }
}
