using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class JornadasCrear
    {
        public int CampeonatoID { get; set; }
        public int CategoriaID { get; set; }
        public int SerieID { get; set; }
        public int Ronda { get; set; }
        public int GrupoJornada { get; set; }
        public int EquipoIDLocal { get; set; }
        public int EquipoIDVisita { get; set; }
        public DateTime FechaInicial { get; set; }
        public int Hora { get; set; }
    }
}
