using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TorneosAdmin.Web.Models
{
    public class Tablero
    {
        public int EquipoID { get; set; }
        public string Equipo { get; set; }
        public byte[] Foto { get; set; }
        public int PartidosJugados { get; set; }
        public int PartidosGanados { get; set; }
        public int PartidosEmpatados { get; set; }
        public int PartidosPerdidos { get; set; }
        public int GolesAFavor { get; set; }
        public int GolesEnContra { get; set; }
        public int GolesDiferencia { get; set; }
        public int Puntos { get; set; }
        public int Posicion { get; set; }
    }
    public class TableroComparer : IEqualityComparer<Tablero>
    {
        public bool Equals([AllowNull] Tablero x, [AllowNull] Tablero y)
        {
            return x.EquipoID == y.EquipoID &&
                x.Equipo == y.Equipo &&
                x.PartidosJugados == y.PartidosJugados &&
                x.PartidosGanados == y.PartidosGanados &&
                x.PartidosEmpatados == y.PartidosEmpatados &&
                x.PartidosPerdidos == y.PartidosPerdidos &&
                x.GolesAFavor == y.GolesAFavor &&
                x.GolesEnContra == y.GolesEnContra &&
                x.GolesDiferencia == y.GolesDiferencia &&
                x.Puntos == y.Puntos &&
                x.Posicion == y.Posicion;
        }

        public int GetHashCode([DisallowNull] Tablero obj)
        {
            return obj.EquipoID.GetHashCode() ^
                obj.Equipo.GetHashCode() ^
                obj.PartidosJugados.GetHashCode() ^
                obj.PartidosGanados.GetHashCode() ^
                obj.PartidosEmpatados.GetHashCode() ^
                obj.PartidosPerdidos.GetHashCode() ^
                obj.GolesAFavor.GetHashCode() ^
                obj.GolesEnContra.GetHashCode() ^
                obj.GolesDiferencia.GetHashCode() ^
                obj.Puntos.GetHashCode() ^
                obj.Posicion.GetHashCode();
        }
    }
}
