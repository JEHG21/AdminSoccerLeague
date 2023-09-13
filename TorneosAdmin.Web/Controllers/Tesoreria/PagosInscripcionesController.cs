using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Identidad;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class PagosInscripcionesController : Controller
    {
        private readonly ModelEntities _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PagosInscripcionesController(ModelEntities context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public JsonResult ObtenerPagosIncripciones(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var inscripcionesLista = from pi in _context.PagosInscripciones
                                     join p1 in _context.Pagos on pi.PagoIDInscripcion equals p1.ID
                                     join p2 in _context.Pagos on pi.PagoIDInscripcion equals p2.ID
                                     select new PagosInscripcionesVista()
                                     {
                                         ID = pi.ID,
                                         CampeonatoID = pi.CampeonatoID,
                                         EquipoID = pi.EquipoID,
                                         MontoGarantia = p1.Monto,
                                         MontoInscripcion = p2.Monto,
                                         Observacion = pi.Observacion
                                     };

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
        public async Task<IActionResult> Crear([Bind("CampeonatoID, EquipoID, MontoInscripcion, MontoGarantia, Observacion")] PagosInscripcionesVista pagosInscripcionesVista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!PagoInscripcionExiste(pagosInscripcionesVista.CampeonatoID, pagosInscripcionesVista.EquipoID))
                {
                    Pagos pagosInscripcion = new Pagos
                    {
                        TipoPagoID = 1,
                        Monto = pagosInscripcionesVista.MontoInscripcion,
                        UsuarioIDCreacion = Convert.ToInt32(_userManager.GetUserId(User)),
                        FechaCreacion = DateTime.Now
                    };

                    _context.Pagos.Add(pagosInscripcion);
                    _context.SaveChanges();

                    Pagos pagosGarantia = new Pagos
                    {
                        TipoPagoID = 2,
                        Monto = pagosInscripcionesVista.MontoGarantia,
                        UsuarioIDCreacion = Convert.ToInt32(_userManager.GetUserId(User)),
                        FechaCreacion = DateTime.Now
                    };

                    _context.Pagos.Add(pagosGarantia);
                    _context.SaveChanges();

                    PagosInscripciones pagosInscripciones = new PagosInscripciones()
                    {
                        CampeonatoID = pagosInscripcionesVista.CampeonatoID,
                        EquipoID = pagosInscripcionesVista.EquipoID,
                        PagoIDGarantia = pagosGarantia.ID,
                        PagoIDInscripcion = pagosInscripcion.ID,
                        Observacion = pagosInscripcionesVista.Observacion
                    };

                    _context.PagosInscripciones.Add(pagosInscripciones);
                    await _context.SaveChangesAsync();
                }
                else
                    return BadRequest("El pago al campeonato de este equipo ya fue realizado. Seleccione otros datos");
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

            return Ok(pagosInscripcionesVista);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("CampeonatoID, EquipoID, MontoInscripcion, MontoGarantia, Observacion")] PagosInscripcionesVista pagosInscripcionesVista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PagosInscripciones entidad = _context.PagosInscripciones.Find(id);
                if (!(entidad.CampeonatoID == pagosInscripcionesVista.CampeonatoID && entidad.EquipoID == pagosInscripcionesVista.EquipoID))
                {
                    if (PagoInscripcionExiste(pagosInscripcionesVista.CampeonatoID, pagosInscripcionesVista.EquipoID))
                    {
                        return BadRequest("El pago al campeonato de este equipo ya fue realizado. Seleccione otros datos");
                    }
                }

                Pagos pagosInscripcion = _context.Pagos.Find(entidad.PagoIDInscripcion);
                pagosInscripcion.Monto = pagosInscripcionesVista.MontoInscripcion;
                pagosInscripcion.UsuarioIDActualizacion = Convert.ToInt32(_userManager.GetUserId(User));
                pagosInscripcion.FechaActulizacion = DateTime.Now;

                Pagos pagosGarantia = _context.Pagos.Find(entidad.PagoIDGarantia);
                pagosGarantia.Monto = pagosInscripcionesVista.MontoGarantia;
                pagosGarantia.UsuarioIDActualizacion = Convert.ToInt32(_userManager.GetUserId(User));
                pagosGarantia.FechaActulizacion = DateTime.Now;

                entidad.CampeonatoID = pagosInscripcionesVista.CampeonatoID;
                entidad.EquipoID = pagosInscripcionesVista.EquipoID;
                entidad.Observacion = pagosInscripcionesVista.Observacion;

                _context.PagosInscripciones.Update(entidad);
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
                PagosInscripciones entidad = _context.PagosInscripciones.Find(id);
                _context.PagosInscripciones.Remove(entidad);
                _context.SaveChanges();

                Pagos pagosInscripcion = _context.Pagos.Find(entidad.PagoIDInscripcion);
                _context.Pagos.Remove(pagosInscripcion);

                Pagos pagosGarantia = _context.Pagos.Find(entidad.PagoIDGarantia);
                _context.Pagos.Remove(pagosGarantia);

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
        private bool PagoInscripcionExiste(int campeonantoID, int equipoID)
        {
            return _context.PagosInscripciones.Any(e => e.CampeonatoID == campeonantoID && e.EquipoID == equipoID);
        }
    }
}