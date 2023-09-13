using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class Equipos
    {
        [Column("EquipoID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int? LigaID { get; set; }

        public int? SerieID { get; set; }

        public int? CategoriaID { get; set; }

        public int? DirigenteID { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el color.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha inicio de fundación.")]
        public DateTime FechaFundacion { get; set; }

        public bool Estado { get; set; }

        public byte[] Foto { get; set; }

        [NotMapped]
        public string NombreArchivo { get; set; }
    }
}
