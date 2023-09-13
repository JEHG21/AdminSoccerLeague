using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class Jugadores
    {
        [Column("JugadorID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int? EquipoID { get; set; }

        public int EstadoCivilID { get; set; }

        public int InstruccionID { get; set; }

        public int ProfesionID { get; set; }

        public int ProvinciaID { get; set; }

        public int ParroquiaID { get; set; }

        [Required(ErrorMessage = "Ingrese la dedula.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el apellido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha inicio de nacimiento.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Ingrese el carnet.")]
        public int Carnet { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha inicio de afiliaxción.")]
        public DateTime FechaAfiliacion { get; set; }

        public bool Calificado { get; set; }
        public bool Estado { get; set; }
        public byte[] Foto { get; set; }

        [NotMapped]
        public string NombreArchivo { get; set; }
    }
}
