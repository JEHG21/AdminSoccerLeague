using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class PartidosCarga
    {
        public int CampeonatoID { get; set; }
        public int CategoriaID { get; set; }
        public int SerieID { get; set; }
        public int Ronda { get; set; }
        public int Fecha { get; set; }
    }
}
