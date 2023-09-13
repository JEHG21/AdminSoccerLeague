using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class Jornadas
    {
        [Column("JornadaID"), ScaffoldColumn(false)]
        public int ID { get; set; }

        public int CampeonatoID { get; set; }

        public int CategoriaID { get; set; }

        public int SerieID { get; set; }

        public int PartidoID { get; set; }

        public int EquipoIDLocal { get; set; }

        public int EquipoIDVisita { get; set; }

        public int GrupoJornada { get; set; }

        public int Ronda { get; set; }
    }
}
