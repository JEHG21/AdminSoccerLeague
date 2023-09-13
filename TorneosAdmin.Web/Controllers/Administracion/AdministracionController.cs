using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class AdministracionController : Controller
    {
        private readonly ModelEntities _context;

        public AdministracionController(ModelEntities context)
        {
            _context = context;
        }

        public IActionResult Permisos()
        {
            ViewBag.RolesLista = _context.Roles.Where(x => x.Estado == true && x.ID != 1);
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult Usuarios()
        {
            return View();
        }

        public IActionResult UsuariosRoles()
        {
            var lista1 = _context.Usuarios.ToDictionary(mc => mc.ID.ToString(), mc => mc.Usuario, StringComparer.OrdinalIgnoreCase);
            var lista2 = _context.Roles.ToDictionary(mc => mc.ID.ToString(), mc => mc.Descripcion, StringComparer.OrdinalIgnoreCase);
            ViewBag.UsuariosLista = JsonSerializer.Serialize(lista1);
            ViewBag.RolesLista = JsonSerializer.Serialize(lista2);
            return View();
        }
    }
}