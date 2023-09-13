using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class Usuarios
    {
        [Column("UsuarioID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        //[Required(ErrorMessage = "Ingrese el usuario.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el apellido paterno.")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Ingrese el apellido materno.")]
        public string ApellidoMaterno { get; set; }

        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Ingrese el correo electrónico.")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Correo electrónico inválido")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Ingrese el telefono.")]
        public string Telefono { get; set; }

        public bool Estado { get; set; }

        public bool PrimerInicio { get; set; }

        public bool Bloqueo { get; set; }

        public int Intentos { get; set; }
    }
}
