using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class RecibosController : Controller
    {
        private readonly ModelEntities _context;

        public RecibosController(ModelEntities context)
        {
            _context = context;
        }
        public IActionResult PagoInscripciones(string id)
        {
            int idPagoInscrpcion = Convert.ToInt32(id, 16);

            var recibo = from pi in _context.PagosInscripciones
                         join cap in _context.Campeonatos on pi.CampeonatoID equals cap.ID
                         join paG in _context.Pagos on pi.PagoIDGarantia equals paG.ID
                         join paI in _context.Pagos on pi.PagoIDInscripcion equals paI.ID
                         join e in _context.Equipos on pi.EquipoID equals e.ID
                         join cat in _context.Categorias on e.CategoriaID equals cat.ID
                         join serie in _context.Series on e.SerieID equals serie.ID
                         join dir in _context.Dirigentes on e.DirigenteID equals dir.ID
                         where pi.ID == idPagoInscrpcion
                         select new ReciboInscripcion
                         {
                             ID = pi.ID,
                             Campeonato = cap.Nombre,
                             FotoEquipo = e.Foto,
                             NombreEquipo = e.Nombre,
                             Categoria = cat.Nombre + " - " + serie.Nombre,
                             Dirigente = dir.Nombre + " " + dir.Apellido,
                             MontoGarantia = paG.Monto,
                             MontoInscripcion = paI.Monto,
                             Observaciones = pi.Observacion
                         };

            return new ViewAsPdf("PagoInscripciones", recibo.FirstOrDefault());
            //return View();
        }
    }
}