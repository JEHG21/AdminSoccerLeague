using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class JornadaEditar
    {
        public int ID { get; set; }
        public int Grupo { get; set; }
        public DateTime Fecha { get; set; }
        public int Hora { get; set; }
    }
}
