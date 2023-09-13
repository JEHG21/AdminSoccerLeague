using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class Arbitros
    {
        [Column("ArbitroID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Ingrese la dedula.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el apellido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese la dirección.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese el país.")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Ingrese el teléfono.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Ingrese el correo electrónico.")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Correo electrónico inválido")]
        public string CorreoElectronico { get; set; }

        public bool Estado { get; set; }

        public byte[] Foto { get; set; }

        [NotMapped]
        public string NombreArchivo { get; set; }
    }
}
