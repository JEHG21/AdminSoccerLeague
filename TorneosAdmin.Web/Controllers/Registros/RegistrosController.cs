using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class RegistrosController : Controller
    {
        private readonly ModelEntities _context;

        public RegistrosController(ModelEntities context)
        {
            _context = context;
        }

        public IActionResult Arbitros()
        {
            return View();
        }

        public IActionResult Campeonatos()
        {
            return View();
        }

        public IActionResult Calificacion()
        {
            var lista1 = _context.Ligas.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista2 = _context.Dirigentes.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre + " " + mc.Apellido, StringComparer.OrdinalIgnoreCase);
            var lista3 = _context.Equipos.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista4 = _context.EstadosCiviles.ToDictionary(mc => mc.ID.ToString(), mc => mc.Descripcion, StringComparer.OrdinalIgnoreCase);
            var lista5 = _context.Instrucciones.ToDictionary(mc => mc.ID.ToString(), mc => mc.Descripcion, StringComparer.OrdinalIgnoreCase);
            var lista6 = _context.Profesiones.ToDictionary(mc => mc.ID.ToString(), mc => mc.Descripcion, StringComparer.OrdinalIgnoreCase);
            var lista7 = _context.Provincias.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista8 = _context.Parroquias.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            
            ViewBag.LigasLista = JsonSerializer.Serialize(lista1);
            ViewBag.DirigentesLista = JsonSerializer.Serialize(lista2);
            ViewBag.EquiposLista = JsonSerializer.Serialize(lista3);
            ViewBag.EstadosCivilesLista = JsonSerializer.Serialize(lista4);
            ViewBag.InstruccionesLista = JsonSerializer.Serialize(lista5);
            ViewBag.ProfesionesLista = JsonSerializer.Serialize(lista6);
            ViewBag.ProvinciasLista = JsonSerializer.Serialize(lista7);
            ViewBag.ParroquiasLista = JsonSerializer.Serialize(lista8);
            return View();
        }

        public IActionResult Equipos()
        {
            var lista1 = _context.Ligas.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista2 = _context.Series.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista3 = _context.Categorias.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista4 = _context.Dirigentes.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre + " " + mc.Apellido, StringComparer.OrdinalIgnoreCase);
            ViewBag.LigasLista = lista1;
            ViewBag.SeriesLista = lista2;
            ViewBag.CategoriasLista = lista3;
            ViewBag.DirigentesLista = lista4;
            var lista5 = _context.Equipos.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista6 = _context.EstadosCiviles.ToDictionary(mc => mc.ID.ToString(), mc => mc.Descripcion, StringComparer.OrdinalIgnoreCase);
            var lista7 = _context.Instrucciones.ToDictionary(mc => mc.ID.ToString(), mc => mc.Descripcion, StringComparer.OrdinalIgnoreCase);
            var lista8 = _context.Profesiones.ToDictionary(mc => mc.ID.ToString(), mc => mc.Descripcion, StringComparer.OrdinalIgnoreCase);
            var lista9 = _context.Provincias.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista10 = _context.Parroquias.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista11 = _context.Parroquias.Select(x => new { Provincia = x.ProvinciaID.ToString(), Parroquia = x.ID.ToString() }).ToList();
            ViewBag.EquiposLista = JsonSerializer.Serialize(lista5);
            ViewBag.EstadosCivilesLista = JsonSerializer.Serialize(lista6);
            ViewBag.InstruccionesLista = JsonSerializer.Serialize(lista7);
            ViewBag.ProfesionesLista = JsonSerializer.Serialize(lista8);
            ViewBag.ProvinciasLista = JsonSerializer.Serialize(lista9);
            ViewBag.ParroquiasLista = JsonSerializer.Serialize(lista10);
            ViewBag.RelacionProParLista = JsonSerializer.Serialize(lista11);
            return View();
        }

        public IActionResult Dirigentes()
        {
            return View();
        }
    }
}