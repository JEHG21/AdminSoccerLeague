using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class PagosInscripcionesVista
    {
        public int ID { get; set; }

        public int CampeonatoID { get; set; }

        public int EquipoID { get; set; }

        [Required(ErrorMessage = "Ingrese el monto de inscripción.")]
        public decimal MontoInscripcion { get; set; }

        [Required(ErrorMessage = "Ingrese el monto de garantía.")]
        public decimal MontoGarantia { get; set; }

        public string Observacion { get; set; }
    }
}
