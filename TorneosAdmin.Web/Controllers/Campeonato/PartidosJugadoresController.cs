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
    public class PartidosJugadoresController : Controller
    {
        private readonly ModelEntities _context;

        public PartidosJugadoresController(ModelEntities context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear( List<PartidosJugadores> jugadores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var jugadoresDuplicdos = jugadores.Where(x => x.JugadorID != 0)
                                                  .GroupBy(s => s.JugadorID)
                                                  .SelectMany(grp => grp.Skip(1));
                if(jugadoresDuplicdos.Count() > 0 )
                {
                    return BadRequest("Los jugadores registrados no deben de ser duplicados.");
                }

                var jornada = _context.Jornadas.FirstOrDefault(x => x.PartidoID == jugadores.First().PartidoID);

                if (jugadores.Where(x => x.JugadorID != 0 && x.Cambio == false && x.EquipoID == jornada.EquipoIDLocal).Count() < 8)
                {
                    return BadRequest("El equipo local deben contener minimo 8 jugadores registrados.");
                }

                if (jugadores.Where(x => x.JugadorID != 0 && x.Cambio == false && x.EquipoID == jornada.EquipoIDVisita).Count() < 8)
                {
                    return BadRequest("El equipo visitante deben contener minimo 8 jugadores resgistrados.");
                }

                var jugadoresNuevos = jugadores.Where(x => x.JugadorID != 0 && x.ID == 0).ToList();
                _context.PartidosJugadores.AddRange(jugadoresNuevos);

                var jugadoresActulizar = jugadores.Where(x => x.JugadorID != 0 && x.ID != 0).ToList();
                _context.PartidosJugadores.UpdateRange(jugadoresActulizar);

                var jugadoresEliminados = jugadores.Where(x => x.JugadorID == 0 && x.ID != 0).ToList();
                _context.PartidosJugadores.RemoveRange(jugadoresEliminados);

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