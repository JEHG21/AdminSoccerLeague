using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class Inscripciones
    {
        [Column("InscripcionID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int CampeonatoID { get; set; }

        public int EquipoID { get; set; }

        public int PagoInscripcionID { get; set; }

        public int UsuarioID { get; set; }

        public DateTime FechaInscripcion { get; set; }
    }
}
