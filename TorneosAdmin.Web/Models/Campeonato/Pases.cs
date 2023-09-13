using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class Pases
    {
        [Column("PaseID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int JugadorID { get; set; }

        public DateTime Fecha { get; set; }

        public int EquipoIN { get; set; }

        public int EquipoOUT { get; set; }

        public Decimal Valor { get; set; }
    }
}
