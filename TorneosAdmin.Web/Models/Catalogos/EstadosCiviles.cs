using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class EstadosCiviles
    {
        [Column("EstadoCivilID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripción del estado civil.")]
        public string Descripcion { get; set; }
    }
}
