using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class Dirigentes
    {
        [Column("DirigenteID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Ingrese la dedula.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el apellidp.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese la dirección.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese el telefono.")]
        public string Telefono { get; set; }

        public bool Estado { get; set; }

        public byte[] Foto { get; set; }

        [NotMapped]
        public string NombreArchivo { get; set; }
    }
}
