using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class PagosInscripciones
    {
        [Column("PagoInscripcionID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int CampeonatoID { get; set; }

        public int EquipoID { get; set; }

        public int PagoIDInscripcion { get; set; }

        public int PagoIDGarantia { get; set; }

        public string Observacion { get; set; }
    }
}
