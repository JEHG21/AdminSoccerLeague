using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers.Campeonato
{
    public class PartidosController : Controller
    {
        private readonly ModelEntities _context;

        public PartidosController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerPartidos(string si1dx, string sort, int page, int rows, [Bind("CampeonatoID, CategoriaID, SerieID, Ronda, Fecha")]PartidosCarga partidosCarga)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var partidosLista = from j in _context.Jornadas
                                join eLocal in _context.Equipos on j.EquipoIDLocal equals eLocal.ID
                                join eVisita in _context.Equipos on j.EquipoIDVisita equals eVisita.ID
                                join p in _context.Partidos on j.PartidoID equals p.ID
                                where j.CampeonatoID == partidosCarga.CampeonatoID && j.CategoriaID == partidosCarga.CategoriaID &&
                                      j.SerieID == partidosCarga.SerieID && j.Ronda == partidosCarga.Ronda && j.GrupoJornada == partidosCarga.Fecha
                                select new
                                {
                                    p.ID,
                                    p.PartidoEstadoID,
                                    Partido = eLocal.Nombre + " VS " + eVisita.Nombre,
                                    p.ArbitroIDCentral,
                                    p.ArbitroIDLateraDerecho,
                                    p.ArbitroIDLateralIzquierdo,
                                    p.VocalEquipoLocal,
                                    p.VocalEquipoVisitante
                                };

            int totalRecords = partidosLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                partidosLista = partidosLista.OrderByDescending(t => t.ID);
                partidosLista = partidosLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                partidosLista = partidosLista.OrderBy(t => t.ID);
                partidosLista = partidosLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = partidosLista
            };
            return Json(jsonData);
        }

        [HttpGet]
        public JsonResult ObtenerPartidoJugadores(int partidoID, int equipoID)
        {
            return Json(_context.PartidosJugadores.Where(x => x.PartidoID == partidoID && x.EquipoID == equipoID));
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("ArbitroIDCentral, ArbitroIDLateraDerecho, ArbitroIDLateralIzquierdo, VocalEquipoLocal, VocalEquipoVisitante")] Partidos partidos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (partidos.ArbitroIDCentral == partidos.ArbitroIDLateraDerecho || 
                    partidos.ArbitroIDLateraDerecho == partidos.ArbitroIDLateralIzquierdo || 
                    partidos.ArbitroIDLateralIzquierdo == partidos.ArbitroIDCentral)
                {
                    return BadRequest("No se puede repetir árbitro.");
                }

                Partidos entidad = _context.Partidos.Find(id);

                if (entidad.PartidoEstadoID != 1)
                {
                    return BadRequest("Partido no se puede configurar debido a que ya fue jugado.");
                }

                entidad.ArbitroIDCentral = partidos.ArbitroIDCentral == 0 ? null : partidos.ArbitroIDCentral;
                entidad.ArbitroIDLateraDerecho = partidos.ArbitroIDLateraDerecho == 0 ? null : partidos.ArbitroIDLateraDerecho;
                entidad.ArbitroIDLateralIzquierdo = partidos.ArbitroIDLateralIzquierdo == 0 ? null : partidos.ArbitroIDLateralIzquierdo;
                entidad.VocalEquipoLocal = partidos.VocalEquipoLocal;
                entidad.VocalEquipoVisitante = partidos.VocalEquipoVisitante;


                _context.Partidos.Update(entidad);
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

        [HttpPut]
        public async Task<IActionResult> ActualizarEstatusPartido(int partidoID, int partidoEstadoID, string observaciones)
        {
            try
            {
                if (partidoEstadoID == 1)
                {
                    return BadRequest("No se puede actualizar y dejar el estado del partido como \"No Jugado\", favor de cambiar el estado.");
                }

                Partidos entidad = _context.Partidos.Find(partidoID);

                entidad.PartidoEstadoID = partidoEstadoID;
                entidad.Observaciones = observaciones == null ? "" : observaciones.Trim();

                _context.Partidos.Update(entidad);
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
    }
}