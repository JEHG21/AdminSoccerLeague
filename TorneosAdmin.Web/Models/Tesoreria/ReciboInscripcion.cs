using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class ReciboInscripcion
    {
        public int ID { get; set; }
        public string Campeonato { get; set; }
        public byte[] FotoEquipo { get; set; }
        public string NombreEquipo { get; set; }
        public string Dirigente { get; set; }
        public string Categoria { get; set; }
        public decimal MontoGarantia { get; set; }
        public decimal MontoInscripcion { get; set; }
        public string Observaciones { get; set; }
    }
}
