using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace APINotificacionesV2.Services
{
    public class NotificationService
    {
        public static async Task<string> WriteTempJsonFile()
        {
            string secretJson = await APINotificacionesV2.ExternalServices.KMSFunctions.getSecretFirebase();
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, secretJson);
            return tempFilePath;
        }

        public static  void DeleteTempJsonFile(string tempFilePath)
        {
            if (File.Exists(tempFilePath))
            {
                 File.Delete(tempFilePath);
            }
        }

        public static async Task FirebaseInstance()
        {
            //Como se espera que se le pase una ruta con el json, escribimos en un archivo el secret json y luego lo borramos.

            string tempFilePath = await WriteTempJsonFile();

            try
            {
                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(tempFilePath)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                File.Delete(tempFilePath);
            }

             
        }


        public static async Task<string> SendNotification(Notificaciones notificationes)
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








