using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Identidad;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly ModelEntities _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InscripcionesController(ModelEntities context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public JsonResult ObtenerInscripciones(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var inscripcionesLista = _context.Inscripciones.Select(x => new
            {
                x.ID,
                x.UsuarioID,
                x.CampeonatoID,
                x.EquipoID,
                x.FechaInscripcion
            });
            int totalRecords = inscripcionesLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                inscripcionesLista = inscripcionesLista.OrderByDescending(t => t.CampeonatoID);
                inscripcionesLista = inscripcionesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                inscripcionesLista = inscripcionesLista.OrderBy(t => t.CampeonatoID);
                inscripcionesLista = inscripcionesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = inscripcionesLista
            };
            return Json(jsonData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("CampeonatoID, EquipoID")] Inscripciones inscripciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!InscripcionExiste(inscripciones.CampeonatoID, inscripciones.EquipoID))
                {
                    if (PagoExiste(inscripciones.CampeonatoID, inscripciones.EquipoID))
                    {
                        var pago = _context.PagosInscripciones.First(x => x.CampeonatoID == inscripciones.CampeonatoID && x.EquipoID == inscripciones.EquipoID);

                        inscripciones.PagoInscripcionID = pago.ID;
                        inscripciones.UsuarioID = Convert.ToInt32(_userManager.GetUserId(User));
                        inscripciones.FechaInscripcion = DateTime.Today;

                        _context.Inscripciones.Add(inscripciones);
                        await _context.SaveChangesAsync();
                    }
                    else
                        return BadRequest("La inscripcion al campeonato de este equipo no tiene pago. Seleccione otros datos");
                }
                else
                    return BadRequest("La inscripcion al campeonato de este equipo ya fue realizado. Seleccione otros datos");
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

            return Ok(inscripciones);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("CampeonatoID, EquipoID")] Inscripciones inscripciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Inscripciones entidad = _context.Inscripciones.Find(id);
                if (!(entidad.CampeonatoID == inscripciones.CampeonatoID && entidad.EquipoID == inscripciones.EquipoID))
                {
                    if (InscripcionExiste(inscripciones.CampeonatoID, inscripciones.EquipoID))
                    {
                        return BadRequest("La inscripcion al campeonato de este equipo ya fue realizado. Seleccione otros datos");
                    }
                    
                    if (!PagoExiste(inscripciones.CampeonatoID, inscripciones.EquipoID)) {

                        return BadRequest("La inscripcion al campeonato de este equipo no tiene pago. Seleccione otros datos");
                    }
                }
                var pago = _context.PagosInscripciones.First(x => x.CampeonatoID == inscripciones.CampeonatoID && x.EquipoID == inscripciones.EquipoID);

                entidad.CampeonatoID = inscripciones.CampeonatoID;
                entidad.EquipoID = inscripciones.EquipoID;
                entidad.PagoInscripcionID = pago.ID;
                entidad.UsuarioID = Convert.ToInt32(_userManager.GetUserId(User));
                entidad.FechaInscripcion = DateTime.Today;

                _context.Inscripciones.Update(entidad);
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

            return Ok("Registro Actualizado");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Inscripciones entidad = _context.Inscripciones.Find(id);

                _context.Inscripciones.Remove(entidad);
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
        private bool InscripcionExiste(int campeonatoID, int equipoID)
        {
            return _context.Inscripciones.Any(e => e.CampeonatoID == campeonatoID && e.EquipoID == equipoID);
        }

        [NonAction]
        private bool PagoExiste(int campeonatoID, int equipoID)
        {
            return _context.PagosInscripciones.Any(e => e.CampeonatoID == campeonatoID && e.EquipoID == equipoID);
        }
    }
}