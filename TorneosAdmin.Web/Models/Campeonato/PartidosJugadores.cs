using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class PartidosJugadores
    {
        [Column("PartidoJugadorID"), ScaffoldColumn(false)]
        public int ID { get; set; }
        public int PartidoID { get; set; }
        public int EquipoID { get; set; }
        public int JugadorID { get; set; }
        public int Goles { get; set; }
        public int TarjetaAmarilla { get; set; }
        public int TarjetaRoja { get; set; }
        public bool Cambio { get; set; }
    }
}
