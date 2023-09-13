using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class PermisosController : Controller
    {
        private readonly ModelEntities _context;

        public PermisosController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerPermisos(int rolID)
        {
            List<PermisosViewModel> listaPantallas = _context.Menus.Select(x => new PermisosViewModel()
            {
                MenuID = x.ID,
                MenuNombre = x.Titulo,
                MenuDescripcion = x.Descripcion
            }).ToList();

            var permisos = _context.Permisos.Where(x => x.RolID == rolID).ToList();

            foreach (var item in listaPantallas)
            {
                if (permisos.Any(x => x.MenuID == item.MenuID))
                    item.Aplicado = true;
            }

            return Json(listaPantallas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarPermiso(int id, [Bind("MenuID, Aplicado")] PermisosViewModel permisos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string mensaje = string.Empty;

            try
            {
                if (permisos.Aplicado)
                {
                    Permisos entidad = new Permisos()
                    {
                        MenuID = permisos.MenuID,
                        RolID = id
                    };
                    _context.Permisos.Add(entidad);

                    mensaje = "Se registro correctamente el permiso.";
                }
                else
                {
                    Permisos entidad = _context.Permisos.First(x => x.RolID == id && x.MenuID == permisos.MenuID);
                    _context.Permisos.Remove(entidad);
                    mensaje = "Se registro correctamente la baja del permiso.";
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string errMsg = FormateadorCadenas.ObtenerMensajesErrores(ex);
                return BadRequest(errMsg);
            }
            catch (Exception ex)
            {
                string errMsg = FormateadorCadenas.ObtenerMensajesErrores(ex);
                return BadRequest(errMsg);
            }

            return Ok(mensaje);
        }
    }
}