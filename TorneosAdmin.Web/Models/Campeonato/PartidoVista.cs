using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class PartidoVista
    {
        public int EquipolocalID { get; set; }
        public string Equipolocal { get; set; }
        public byte[] EquipolocalImg { get; set; }
        public string EquipolocalVocal { get; set; }
        public int EquipoVisitaID { get; set; }
        public string EquipoVisita { get; set; }
        public byte[] EquipoVisitaImg { get; set; }
        public string EquipoVisitaVocal { get; set; }
        public int PartidoID { get; set; }
        public string ArbitroCentral { get; set; }
        public string ArbitroDerecho { get; set; }
        public string ArbitroIzquierdo { get; set; }
        public string FechaPartido { get; set; }
        public string HoraPartido { get; set; }
        public int PartidoEstadoID { get; set; }
        public string Observaciones { get; set; }
    }
}
