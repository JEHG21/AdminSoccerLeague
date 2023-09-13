using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class Partidos
    {
        [Column("PartidoID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int PartidoEstadoID { get; set; }

        public int? ArbitroIDCentral { get; set; }

        public int? ArbitroIDLateraDerecho { get; set; }

        public int? ArbitroIDLateralIzquierdo { get; set; }

        public string VocalEquipoLocal { get; set; }

        public string VocalEquipoVisitante { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha del partido.")]
        public DateTime FechaHora { get; set; }

        public string Observaciones { get; set; }
    }
}
