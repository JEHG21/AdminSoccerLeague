using System.ComponentModel.DataAnnotations;

namespace TorneosAdmin.Web.Models
{
    public class Acceso
    {
        [Required(ErrorMessage = "Ingrese usuario."), Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Ingrese contraseña"), Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        public bool Recuerdame { get; set; }
    }
}
