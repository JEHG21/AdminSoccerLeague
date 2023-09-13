using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class Roles
    {
        [Column("RolID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Debe ingresar descripción")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }
    }
}
