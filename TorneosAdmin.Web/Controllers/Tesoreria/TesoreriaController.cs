using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TorneosAdmin.Web.Identidad;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class TesoreriaController : Controller
    {
        private readonly ModelEntities _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TesoreriaController(ModelEntities context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult PagosInscripciones()
        {
            var lista1 = _context.Campeonatos.Where(x => x.Estado == true).ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            var lista2 = _context.Equipos.Where(x => x.Estado == true).ToDictionary(mc => mc.ID.ToString(), mc => mc.Nombre, StringComparer.OrdinalIgnoreCase);
            ViewBag.CampeonatosLista = JsonSerializer.Serialize(lista1);
            ViewBag.EquiposLista = JsonSerializer.Serialize(lista2);

            return View();
        }

        public IActionResult PagosMultas()
        {
            return View();
        }

        public IActionResult PagosSanciones()
        {
            return View();
        }
    }
}