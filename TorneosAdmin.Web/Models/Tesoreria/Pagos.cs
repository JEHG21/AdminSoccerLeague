using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class Pagos
    {
        [Column("PagoID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int TipoPagoID { get; set; }

        public decimal Monto { get; set; }

        public int UsuarioIDCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioIDActualizacion { get; set; }

        public Nullable<DateTime> FechaActulizacion { get; set; }
    }
}
