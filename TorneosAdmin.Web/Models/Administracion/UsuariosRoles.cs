using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class UsuariosRoles
    {
        [Column("UsuarioRolID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Seleccione un usuario.")]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "Seleccione un rol.")]
        public int RolID { get; set; }
    }
}
