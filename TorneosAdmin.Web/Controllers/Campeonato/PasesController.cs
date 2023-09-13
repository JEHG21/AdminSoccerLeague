using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class PasesController : Controller
    {
        private readonly ModelEntities _context;

        public PasesController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerPases(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var pasesLista = _context.Pases.Select(x => new
            {
                x.ID,
                x.JugadorID,
                x.Fecha,
                x.EquipoIN,
                x.EquipoOUT,
                x.Valor
            });
            int totalRecords = pasesLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                pasesLista = pasesLista.OrderByDescending(t => t.Fecha);
                pasesLista = pasesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                pasesLista = pasesLista.OrderBy(t => t.Fecha);
                pasesLista = pasesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = pasesLista
            };
            return Json(jsonData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("JugadorID, Fecha, EquipoIN, EquipoOUT, Valor")] Pases pases)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (pases.EquipoOUT == pases.EquipoIN)
                {
                    return BadRequest("No se puede pasar el jugador al mismo equipo, seleccione otro equipo.");
                }
                Jugadores jugadores = _context.Jugadores.Find(pases.JugadorID);
                jugadores.EquipoID = pases.EquipoIN;

                _context.Jugadores.Update(jugadores);
                await _context.SaveChangesAsync();

                Pases entidad = new Pases
                {
                    JugadorID = pases.JugadorID,
                    Fecha = pases.Fecha,
                    EquipoIN = pases.EquipoIN,
                    EquipoOUT = pases.EquipoOUT,
                    Valor = pases.Valor
                };

                _context.Pases.Add(entidad);
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

            return Ok(pases);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("JugadorID, Fecha, EquipoIN, EquipoOUT, Valor")] Pases pases)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (pases.EquipoOUT == pases.EquipoIN)
                {
                    return BadRequest("No se puede pasar el jugador al mismo equipo, seleccione otro equipo.");
                }

                int maxID = _context.Pases.Where(x => x.JugadorID == pases.JugadorID).Select(x=> x.ID).Max();

                Pases entidad = _context.Pases.Find(id);

                if (entidad.ID < maxID)
                {
                    return BadRequest("No se puede editar ya que este no es el último movimiento, seleccione otro movimiento.");
                }

                Jugadores jugadores = _context.Jugadores.Find(pases.JugadorID);
                jugadores.EquipoID = pases.EquipoIN;

                _context.Jugadores.Update(jugadores);
                await _context.SaveChangesAsync();

                entidad.Fecha = pases.Fecha;
                entidad.EquipoIN = pases.EquipoIN;
                entidad.Valor = pases.Valor;

                _context.Pases.Update(entidad);
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
                Pases entidad = _context.Pases.Find(id);

                int maxID = _context.Pases.Where(x => x.JugadorID == entidad.JugadorID).Select(x => x.ID).Max();

                if (entidad.ID < maxID)
                {
                    return BadRequest("No se puede eliminar ya que este no es el último movimiento, seleccione otro movimiento.");
                }

                Jugadores jugadores = _context.Jugadores.Find(entidad.JugadorID);
                jugadores.EquipoID = entidad.EquipoOUT;

                _context.Jugadores.Update(jugadores);
                await _context.SaveChangesAsync();

                _context.Pases.Remove(entidad);
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
    }
}