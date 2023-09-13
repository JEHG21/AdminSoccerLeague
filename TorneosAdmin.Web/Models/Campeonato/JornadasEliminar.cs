using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class JornadasEliminar
    {
        public int ID { get; set; }
        public int CampeonatoID { get; set; }
        public int CategoriaID { get; set; }
        public int SerieID { get; set; }
        public int Ronda { get; set; }
    }
}
