using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class CatalogosController : Controller
    {
        private readonly ModelEntities _context;

        public CatalogosController(ModelEntities context)
        {
            _context = context;
        }

        public IActionResult Categorias()
        {
            return View();
        }

        public IActionResult EstadosCiviles()
        {
            return View();
        }

        public IActionResult Instrucciones()
        {
            return View();
        }

        public IActionResult Ligas()
        {
            return View();
        }

        public IActionResult Parroquias()
        {
            var lista1 = _context.Provincias.ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            ViewBag.ProvinciasLista = JsonSerializer.Serialize(lista1);
            return View();
        }

        public IActionResult PartidosEstados()
        {
            return View();
        }

        public IActionResult Profesiones()
        {
            return View();
        }

        public IActionResult Provincias()
        {
            return View();
        }

        public IActionResult Series()
        {
            return View();
        }

        public IActionResult TiposPagos()
        {
            return View();
        }
    }
}