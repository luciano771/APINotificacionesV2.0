using System.ComponentModel.DataAnnotations;

namespace APINotificacionesV2.Models.Entities
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string? NombreUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string? CorreoElectronico { get; set; }

        [Required]
        [StringLength(100)]
        public string Contraseña { get; set; }

        [StringLength(10000)]
        public string? TokenNotificacion { get; set; }

    }
}
