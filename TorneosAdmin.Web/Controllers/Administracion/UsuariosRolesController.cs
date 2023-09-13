using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class UsuariosRolesController : Controller
    {
        private readonly ModelEntities _context;

        public UsuariosRolesController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerUsuarioRoles(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var rolesLista = _context.UsuariosRoles.Where(x => x.RolID != 1).Select(x => new
            {
                x.ID,
                x.UsuarioID,
                x.RolID
            });
            int totalRecords = rolesLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                rolesLista = rolesLista.OrderByDescending(t => t.UsuarioID);
                rolesLista = rolesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                rolesLista = rolesLista.OrderBy(t => t.UsuarioID);
                rolesLista = rolesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = rolesLista
            };
            return Json(jsonData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("UsuarioID,RolID")] UsuariosRoles usuariosRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!UsuarioRoleExiste(usuariosRoles.UsuarioID, usuariosRoles.RolID))
                {
                    _context.UsuariosRoles.Add(usuariosRoles);
                    await _context.SaveChangesAsync();
                }
                else
                    return BadRequest("Esa relación ya existe en el sistema, seleccione otra.");
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

            return Ok(usuariosRoles);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("UsuarioID,RolID")] UsuariosRoles usuariosRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!UsuarioRoleExiste(usuariosRoles.UsuarioID, usuariosRoles.RolID))
                {
                    UsuariosRoles entidad = _context.UsuariosRoles.Find(id);

                    entidad.UsuarioID = usuariosRoles.UsuarioID;
                    entidad.RolID = usuariosRoles.RolID;

                    _context.UsuariosRoles.Update(entidad);
                    await _context.SaveChangesAsync();
                }
                else
                    return BadRequest("Esa relación ya existe en el sistema, seleccione otra.");
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

            return Ok("Registro Actualizado");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UsuariosRoles entidad = _context.UsuariosRoles.Find(id);

                _context.Remove(entidad);
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

            return Ok();
        }

        [NonAction]
        private bool UsuarioRoleExiste(int usuarioId, int rolId)
        {
            return _context.UsuariosRoles.Any(e => e.UsuarioID == usuarioId && e.RolID == rolId);
        }
    }
}