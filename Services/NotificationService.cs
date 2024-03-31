using APINotificacionesV2.Models.Entities;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth.OAuth2;

namespace APINotificacionesV2.Services
{
    public class NotificationService
    {


        public static Task FirebaseInstance()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });
            }

            return Task.CompletedTask;
        }

        public static async Task<String> SendNotification(Notificaciones notificationes)
        {

            await FirebaseInstance();

            var message = new Message()
            {
                Token = notificationes.Token,

                Notification = new Notification()
                {
                    Title = notificationes.Titulo,
                    Body = notificationes.Mensaje,
                    // si por post no mando nada, imageurl dara un exepcion que dice url no valida.  
                    ImageUrl = !string.IsNullOrEmpty(notificationes.Url) && Uri.TryCreate(notificationes.Url, UriKind.Absolute, out _) ? notificationes.Url : "Http://nada.png"

                }
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return response;
        }



       



    }

   
}








