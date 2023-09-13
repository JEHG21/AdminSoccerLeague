using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class JornadasController : Controller
    {
        private readonly ModelEntities _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public JornadasController(ModelEntities context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public JsonResult ObtenerJornadasVista(int campeonatoID, int categoriaID, int serieID )
        {
            var jornadasLista = from j in _context.Jornadas
                               join p in _context.Partidos on j.PartidoID equals p.ID
                               where j.CampeonatoID == campeonatoID && j.CategoriaID == categoriaID && j.SerieID == serieID
                               select new
                               {
                                   j.ID,
                                   j.EquipoIDLocal,
                                   j.EquipoIDVisita,
                                   j.GrupoJornada,
                                   j.Ronda,
                                   PartidoID = p.ID,
                                   FechaPartido = p.FechaHora.ToString("ddd", new CultureInfo("es-EC")) + " " + p.FechaHora.ToString("dd/MM/yyyy"),
                                   HoraPartido = p.FechaHora.ToString("HH:mm")
                               };
            return Json(jornadasLista);
        }

        [HttpGet]
        public JsonResult ObtenerJornadasFechas([Bind("CampeonatoID, CategoriaID, SerieID, Ronda")]PartidosCarga partidosCarga)
        {
            var jornadasFechasLista = _context.Jornadas.Where(x => x.CampeonatoID == partidosCarga.CampeonatoID && 
                                                             x.CategoriaID == partidosCarga.CategoriaID && 
                                                             x.SerieID == partidosCarga.SerieID &&
                                                             x.Ronda == partidosCarga.Ronda)
                                                 .Select(x =>new { ID = x.GrupoJornada, Fecha = "Fecha - " + x.GrupoJornada.ToString()})
                                                 .Distinct();
            return Json(jornadasFechasLista);
        }

        [HttpGet]
        public JsonResult ObtenerJornadasFechasPartidos([Bind("CampeonatoID, CategoriaID, SerieID, Ronda")]PartidosCarga partidosCarga, int fecha)
        {
            var jornadasLista = _context.Jornadas.Where(x => x.CampeonatoID == partidosCarga.CampeonatoID &&
                                                             x.CategoriaID == partidosCarga.CategoriaID &&
                                                             x.SerieID == partidosCarga.SerieID &&
                                                             x.Ronda == partidosCarga.Ronda &&
                                                             x.GrupoJornada == fecha)
                                                 .Select(x => x.ID);
            var jornadasPartidosLista = from j in _context.Jornadas
                                        join el in _context.Equipos on j.EquipoIDLocal equals el.ID
                                        join ev in _context.Equipos on j.EquipoIDVisita equals ev.ID
                                        where jornadasLista.Contains(j.ID)
                                        select new
                                        {
                                            j.ID,
                                            equipos = el.Nombre + " vs " + ev.Nombre
                                        };

            return Json(jornadasPartidosLista);
        }

        [HttpGet]
        public JsonResult ObtenerJornadasPartidoVista(int jornadaID)
        {
            var partido = (from j in _context.Jornadas
                          join el in _context.Equipos on j.EquipoIDLocal equals el.ID
                          join ev in _context.Equipos on j.EquipoIDVisita equals ev.ID
                          join p in _context.Partidos on j.PartidoID equals p.ID
                          where j.ID == jornadaID
                          select new PartidoVista
                          {
                              EquipolocalID = el.ID,
                              Equipolocal = el.Nombre,
                              EquipolocalImg = el.Foto,
                              EquipolocalVocal = p.VocalEquipoLocal,
                              EquipoVisitaID = ev.ID,
                              EquipoVisita = ev.Nombre,
                              EquipoVisitaImg = ev.Foto,
                              EquipoVisitaVocal = p.VocalEquipoVisitante,
                              PartidoID = p.ID,
                              ArbitroCentral = p.ArbitroIDCentral.HasValue == false ? "Sin Árbitro" : p.ArbitroIDCentral.ToString(),
                              ArbitroDerecho = p.ArbitroIDLateraDerecho.HasValue == false ? "Sin Árbitro" : p.ArbitroIDLateraDerecho.ToString(),
                              ArbitroIzquierdo = p.ArbitroIDLateralIzquierdo.HasValue == false ? "Sin Árbitro" : p.ArbitroIDLateralIzquierdo.ToString(),
                              FechaPartido = p.FechaHora.ToString("ddd", new CultureInfo("es-EC")) + " " + p.FechaHora.ToString("dd/MM/yyyy"),
                              HoraPartido = p.FechaHora.ToString("HH:mm"),
                              PartidoEstadoID = p.PartidoEstadoID,
                              Observaciones = p.Observaciones
                          }).FirstOrDefault();

            var arbitroslista = _context.Arbitros.Select(x => new { x.ID, Nombre = (x.Nombre + " " + x.Apellido) }).ToList();

            partido.ArbitroCentral = partido.ArbitroCentral == "Sin Árbitro" ? partido.ArbitroCentral : arbitroslista.FirstOrDefault(x => x.ID == Convert.ToInt32(partido.ArbitroCentral)).Nombre;
            partido.ArbitroDerecho = partido.ArbitroDerecho == "Sin Árbitro" ? partido.ArbitroDerecho : arbitroslista.FirstOrDefault(x => x.ID == Convert.ToInt32(partido.ArbitroDerecho)).Nombre;
            partido.ArbitroIzquierdo = partido.ArbitroIzquierdo == "Sin Árbitro" ? partido.ArbitroIzquierdo : arbitroslista.FirstOrDefault(x => x.ID == Convert.ToInt32(partido.ArbitroIzquierdo)).Nombre;
            return Json(partido);
        }

        [HttpGet]
        public JsonResult ObtenerJornadasTablero([Bind("CampeonatoID, CategoriaID, SerieID, Ronda")]PartidosCarga partidosCarga)
        {
            List<Tablero> tablero = _context.Jornadas.Where(x => x.CampeonatoID == partidosCarga.CampeonatoID &&
                                                                 x.CategoriaID == partidosCarga.CategoriaID &&
                                                                 x.SerieID == partidosCarga.SerieID &&
                                                                 x.Ronda == partidosCarga.Ronda)
                                                     .Select(x => new Tablero
                                                     {
                                                         EquipoID = x.EquipoIDLocal,
                                                         Equipo = "",
                                                         PartidosJugados = 0,
                                                         PartidosGanados = 0,
                                                         PartidosEmpatados = 0,
                                                         PartidosPerdidos = 0,
                                                         GolesAFavor = 0,
                                                         GolesEnContra = 0,
                                                         GolesDiferencia = 0,
                                                         Puntos = 0,
                                                         Posicion = 0,
                                                     }).Distinct().ToList();

            var visita = _context.Jornadas.Where(x => x.CampeonatoID == partidosCarga.CampeonatoID &&
                                                      x.CategoriaID == partidosCarga.CategoriaID &&
                                                      x.SerieID == partidosCarga.SerieID &&
                                                      x.Ronda == partidosCarga.Ronda)
                                          .Select(x => new Tablero
                                          {
                                              EquipoID = x.EquipoIDVisita,
                                              Equipo = "",
                                              PartidosJugados = 0,
                                              PartidosGanados = 0,
                                              PartidosEmpatados = 0,
                                              PartidosPerdidos = 0,
                                              GolesAFavor = 0,
                                              GolesEnContra = 0,
                                              GolesDiferencia = 0,
                                              Puntos = 0,
                                              Posicion = 0,
                                          }).Distinct().ToList();

            tablero.AddRange(visita);
            tablero = tablero.Distinct(new TableroComparer()).ToList();

            //Obtenemos todos los partidos de la ronda
            var partidos = (from j in _context.Jornadas
                            join p in _context.Partidos on j.PartidoID equals p.ID
                            where j.CampeonatoID == partidosCarga.CampeonatoID &&
                                  j.CategoriaID == partidosCarga.CategoriaID &&
                                  j.SerieID == partidosCarga.SerieID &&
                                  j.Ronda == partidosCarga.Ronda &&
                                  (p.PartidoEstadoID == 5 || p.PartidoEstadoID == 6 || p.PartidoEstadoID == 7)
                            select new { p.ID, p.PartidoEstadoID, j.EquipoIDLocal, j.EquipoIDVisita }).ToList();

            foreach (var partido in partidos)
            {
                if(partido.PartidoEstadoID == 5 || partido.PartidoEstadoID == 6 || partido.PartidoEstadoID == 7)
                {
                    // Obtenemos el equipo local
                    var local = tablero.FirstOrDefault(x => x.EquipoID == partido.EquipoIDLocal);
                    
                    // Obtenemos el equipo visitante
                    var visitante = tablero.FirstOrDefault(x => x.EquipoID == partido.EquipoIDVisita);

                    // Registramos el partido jugado en el respectivo equipo
                    local.PartidosJugados++;
                    visitante.PartidosJugados++;

                    // Agrgamos los goles que cada equipo realizo
                    local.GolesAFavor += _context.PartidosJugadores.Where(x => x.PartidoID == partido.ID && x.EquipoID == local.EquipoID).Sum(x => x.Goles);
                    visitante.GolesAFavor += _context.PartidosJugadores.Where(x => x.PartidoID == partido.ID && x.EquipoID == visitante.EquipoID).Sum(x => x.Goles);

                    // Agregamos los goles en contra(los que hizo el equipo contrario)
                    local.GolesEnContra += _context.PartidosJugadores.Where(x => x.PartidoID == partido.ID && x.EquipoID == visitante.EquipoID).Sum(x => x.Goles);
                    visitante.GolesEnContra += _context.PartidosJugadores.Where(x => x.PartidoID == partido.ID && x.EquipoID == local.EquipoID).Sum(x => x.Goles);

                    // Colocamos la diferiencia de goles
                    local.GolesDiferencia = local.GolesAFavor - local.GolesEnContra;
                    visitante.GolesDiferencia = visitante.GolesAFavor - visitante.GolesEnContra;

                    switch (partido.PartidoEstadoID)
                    {
                        // Juego Empatado
                        case 5:
                            visitante.PartidosEmpatados++;
                            local.PartidosEmpatados++;
                            local.Puntos += 1;
                            visitante.Puntos += 1;
                            break;
                        // Gana local
                        case 6:
                            local.PartidosGanados++;
                            visitante.PartidosPerdidos++;
                            local.Puntos += 3;
                            break;
                        // Gana Visitante
                        case 7:
                            visitante.PartidosGanados++;
                            local.PartidosPerdidos++;
                            visitante.Puntos += 3;
                            break;
                    }

                    int indexLocal = tablero.FindIndex(x => x.EquipoID == partido.EquipoIDLocal);
                    tablero[indexLocal] = local;

                    int indexVisitante = tablero.FindIndex(x => x.EquipoID == partido.EquipoIDVisita);
                    tablero[indexVisitante] = visitante;
                }
            }

            //Asignamos la posicion en base a los puntos o por ID y considerando que hayan tenido partidos jugados
            int pos = 1;
            foreach (var equipo in tablero.Where(x => x.PartidosJugados > 0).OrderByDescending(x => x.Puntos).ThenByDescending(x => x.GolesDiferencia))
            {
                equipo.Posicion = pos;
                pos++;
            }

            // Asignamos la posición a los equipos que no hayan jugado en base al ID
            int maxPosicion = tablero.Max(x => x.Posicion);
            maxPosicion = maxPosicion + 1;
            foreach (var equipo in tablero.Where(x => x.Posicion < 1))
            {
                equipo.Posicion = maxPosicion;
                maxPosicion++;
            }

            // Colocamos el nombre la foto
            foreach (var item in tablero)
            {
                item.Equipo = _context.Equipos.Find(item.EquipoID).Nombre;
                item.Foto = _context.Equipos.Find(item.EquipoID).Foto;
            }

            return Json(tablero.OrderBy(x => x.Posicion).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CargaInicial([Bind("CampeonatoID, CategoriaID, SerieID, Ronda, Dias, FechaInicial, Hora")]JornadasCarga jornadasCarga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (jornadasCarga.Dias[0] == null)
            {
                return BadRequest("Deben seleccionar al menos un dia a configurar.");
            }

            var juegos = _context.Jornadas.Where(x =>
                                                 x.CampeonatoID == jornadasCarga.CampeonatoID &&
                                                 x.CategoriaID == jornadasCarga.CategoriaID &&
                                                 x.SerieID == jornadasCarga.SerieID &&
                                                 x.Ronda == jornadasCarga.Ronda).ToList();
            if(juegos.Count>0)
                return BadRequest("Ya existe fechas configuradas.");

            try
            {
                // Obtenemos los dias
                int indice = 0;
                int[] dias = new int[jornadasCarga.Dias.Count()];
                foreach (var item in jornadasCarga.Dias)
                {
                    dias[indice] = Convert.ToInt32(item);
                    indice++;
                }

                // Obtenemos los equipos que se han inscripto en el campeonato
                var equipos = (from e in _context.Equipos
                               join i in _context.Inscripciones on e.ID equals i.EquipoID
                               where i.CampeonatoID == jornadasCarga.CampeonatoID &&
                                     e.CategoriaID == jornadasCarga.CategoriaID &&
                                     e.SerieID == jornadasCarga.SerieID
                               select e).OrderBy(x=> x.ID).ToList();

                if (equipos.Count <= 1)
                    return BadRequest("No existen equipos registrados.");

                // Colocamos todos los id de equipos en una matriz
                indice = 0;
                int[] matriz = new int[equipos.Count()];
                equipos.ForEach(delegate (Equipos equipo)
                {
                    matriz[indice] = equipo.ID;
                    indice++;
                });


                // Se calcula el numero de fechas a realizarse
                int fechas = equipos.Count();
                if ((fechas) % 2 == 0)
                {
                    //Si es numero impar el total de equipos se resta 1
                    fechas = fechas - 1;
                }

                // Obtenemos primer dia de las jornadas.
                DateTime fechaInicial; 
                int dia = (int)jornadasCarga.FechaInicial.DayOfWeek;
                if (dias.Contains(dia))
                {
                    fechaInicial = jornadasCarga.FechaInicial.AddHours(jornadasCarga.Hora);
                }
                else {
                    int i = dias.Max();
                    if (dia > i)
                        fechaInicial = jornadasCarga.FechaInicial.AddDays(7 + (dias.Min() - dia)).AddHours(jornadasCarga.Hora);
                    else {
                        i = 0;
                        for (int x = 0; x < dias.Length; x++)
                        {
                            if (dias[x] > dia)
                            {
                                i = dias[x];
                                break;
                            }
                        }
                        fechaInicial = jornadasCarga.FechaInicial.AddDays(i - dia).AddHours(jornadasCarga.Hora);
                    }
                }
                //Guardamos El dia que iniciamos
                dia = (int)fechaInicial.DayOfWeek;

                //Pasamos todas las jornadas
                for (int x = 1; x <= fechas; x++)
                {
                    int i = 0;

                    // Insertamos en base de datos los registros de las jornadas
                    for (int j = equipos.Count() - 1; i < j; j--)
                    {
                        // Creamos el partido primero
                        Partidos partido = new Partidos
                        {
                            PartidoEstadoID = 1,
                            FechaHora = fechaInicial
                        };
                        _context.Partidos.Add(partido);
                        _context.SaveChanges();

                        //Creamos la jornada y pasamos el id del partido
                        Jornadas jornadas = new Jornadas()
                        {
                            // Le asignamos valores a la jornada
                            CampeonatoID = jornadasCarga.CampeonatoID,
                            PartidoID = partido.ID,
                            EquipoIDLocal = matriz[i],
                            EquipoIDVisita = matriz[j],
                            CategoriaID = jornadasCarga.CategoriaID,
                            SerieID = jornadasCarga.SerieID,
                            GrupoJornada = x,
                            Ronda = jornadasCarga.Ronda
                        };

                        _context.Jornadas.Add(jornadas);
                        _context.SaveChanges();

                        // Por cada partido agregamos 2 horas.
                        fechaInicial = fechaInicial.AddHours(2);
                        i++;
                    }

                    // Se reacomoda la matriz para que el segundo pase a ser el ultimo
                    int ultimaPosicion = matriz[equipos.Count() - 1];
                    for (int y = equipos.Count() - 1; y > 1; y--)
                    {
                        matriz[y] = matriz[y - 1];
                    }
                    matriz[1] = ultimaPosicion;

                    // Reiniciamos el contador de i para nuevamente tomar la primera posición en la tabla
                    i = 0;

                    // Intercambiamos los dias seleccionados, y configurando que sean las 8 de la mañana
                    fechaInicial = fechaInicial.Date;

                    // Tomamos el indice del dia en arreglo del dias y nos pasamos al siguiente
                    int indexdias = Array.IndexOf(dias, dia) + 1;
                    dia = (int)fechaInicial.DayOfWeek;

                    // Si solo se tiene un dia en el arreglo hacemos el ciclo a la siguiente semana
                    if (dias.Length == 1)
                    {
                        if (dias[0] == 6)
                            fechaInicial = fechaInicial.AddDays(dias[0] - dia).AddHours(jornadasCarga.Hora);
                        else
                            fechaInicial = fechaInicial.AddDays(7 + (dias[0] - dia)).AddHours(jornadasCarga.Hora);
                    }
                    else
                    {
                        // Si se pasa del elementos del arreglo tomamos el primero
                        if (indexdias > (dias.Length - 1))
                            fechaInicial = fechaInicial.AddDays(7 + (dias[0] - dia)).AddHours(jornadasCarga.Hora);
                        else
                        {
                            if (dias[indexdias - 1] == 0)
                                fechaInicial = fechaInicial.AddDays(dias[indexdias] - dia).AddHours(jornadasCarga.Hora);
                            else
                                fechaInicial = fechaInicial.AddDays(7 + (dias[indexdias] - dia)).AddHours(jornadasCarga.Hora);
                        }
                    }

                    //Guardamos para el siguiente ciclo
                    dia = (int)fechaInicial.DayOfWeek;
                }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("CampeonatoID, CategoriaID, SerieID, Ronda, GrupoJornada, EquipoIDLocal, EquipoIDVisita , FechaInicial, Hora")]JornadasCrear jornadasCrear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (jornadasCrear.EquipoIDLocal == jornadasCrear.EquipoIDVisita)
                {
                    return BadRequest("El equipo local y visitante no pueden ser el mismo equipo.");
                }

                if (JornadaExiste(jornadasCrear))
                {
                    return BadRequest("Ya existe esta relación de juego, seleccione otros equipos");
                }

                // Creamos el partido 
                Partidos partido = new Partidos
                {
                    PartidoEstadoID = 1,
                    FechaHora = jornadasCrear.FechaInicial.AddHours(jornadasCrear.Hora)
                };
                _context.Partidos.Add(partido);
                _context.SaveChanges();

                //Creamos la jornada y pasamos el id del partido
                Jornadas jornadas = new Jornadas()
                {
                    // Le asignamos valores a la jornada
                    CampeonatoID = jornadasCrear.CampeonatoID,
                    PartidoID = partido.ID,
                    EquipoIDLocal = jornadasCrear.EquipoIDLocal,
                    EquipoIDVisita = jornadasCrear.EquipoIDVisita,
                    CategoriaID = jornadasCrear.CategoriaID,
                    SerieID = jornadasCrear.SerieID,
                    GrupoJornada = jornadasCrear.GrupoJornada,
                    Ronda = jornadasCrear.Ronda
                };

                _context.Jornadas.Add(jornadas);
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

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([Bind("ID, Grupo, Fecha, Hora")]JornadaEditar jornadaEditar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var jornada = _context.Jornadas.Find(jornadaEditar.ID);

                jornada.GrupoJornada = jornadaEditar.Grupo;

                _context.Jornadas.Update(jornada);
                await _context.SaveChangesAsync();

                // TODO:Eliminar todo lo relacionado con partidos previmente.
                var partido = _context.Partidos.Find(jornada.PartidoID);

                partido.FechaHora = jornadaEditar.Fecha.AddHours(jornadaEditar.Hora);

                _context.Partidos.Update(partido);
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
        public async Task<IActionResult> EliminarGrupo([Bind("ID, CampeonatoID, CategoriaID, SerieID, Ronda")]JornadasEliminar jornadasEliminarGrupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var jornadas = _context.Jornadas.Where(x => x.CampeonatoID == jornadasEliminarGrupo.CampeonatoID &&
                                                           x.CategoriaID == jornadasEliminarGrupo.CategoriaID &&
                                                           x.SerieID == jornadasEliminarGrupo.SerieID &&
                                                           x.Ronda == jornadasEliminarGrupo.Ronda &&
                                                           x.GrupoJornada == jornadasEliminarGrupo.ID);

                // TODO:Eliminar todo lo relacionado con partidos previmente.
                var partidos = from p in _context.Partidos
                               join j in _context.Jornadas on p.ID equals j.PartidoID
                               where j.CampeonatoID == jornadasEliminarGrupo.CampeonatoID &&
                                     j.CategoriaID == jornadasEliminarGrupo.CategoriaID &&
                                     j.SerieID == jornadasEliminarGrupo.SerieID &&
                                     j.Ronda == jornadasEliminarGrupo.Ronda &&
                                     j.GrupoJornada == jornadasEliminarGrupo.ID
                               select p;

                _context.Jornadas.RemoveRange(jornadas);
                await _context.SaveChangesAsync();

                _context.Partidos.RemoveRange(partidos);
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
                var jornada = _context.Jornadas.Find(id);

                // TODO:Eliminar todo lo relacionado con partidos previmente.
                var partido = _context.Partidos.Find(jornada.PartidoID);

                _context.Jornadas.Remove(jornada);
                await _context.SaveChangesAsync();

                _context.Partidos.Remove(partido);
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
        private bool JornadaExiste(JornadasCrear jornadasCrear)
        {
            return _context.Jornadas.Any(e => e.CampeonatoID == jornadasCrear.CampeonatoID &&
                                              e.CategoriaID == jornadasCrear.CategoriaID &&
                                              e.SerieID == jornadasCrear.SerieID &&
                                              e.Ronda == jornadasCrear.Ronda &&
                                              e.GrupoJornada == jornadasCrear.GrupoJornada &&
                                              e.EquipoIDLocal == jornadasCrear.EquipoIDLocal &&
                                              e.EquipoIDVisita == jornadasCrear.EquipoIDVisita);
        }
    }
}