using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class JugadoresController : Controller
    {
        private readonly ModelEntities _context;

        public JugadoresController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerJugadores(string si1dx, string sort, int page, int rows, int? equipoID)
        {
            sort = (sort == null) ? "" : sort;
            equipoID = (equipoID == 0) ? null : equipoID;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var jugadoresLista = _context.Jugadores.Where(x => x.EquipoID == equipoID).Select(x => new
            {
                x.ID,
                x.EquipoID,
                x.EstadoCivilID,
                x.InstruccionID,
                x.ProfesionID,
                x.ProvinciaID,
                x.ParroquiaID,
                x.Cedula,
                x.Nombre,
                x.Apellido,
                x.FechaNacimiento,
                x.Carnet,
                x.FechaAfiliacion,
                x.Estado,
                x.Calificado,
                x.Foto
            });
            int totalRecords = jugadoresLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                jugadoresLista = jugadoresLista.OrderByDescending(t => t.Nombre);
                jugadoresLista = jugadoresLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                jugadoresLista = jugadoresLista.OrderBy(t => t.Nombre);
                jugadoresLista = jugadoresLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = jugadoresLista
            };
            return Json(jsonData);
        }

        [HttpGet]
        public JsonResult ObtenerJugadoresEquipo(int jugadorID)
        {
            return Json(_context.Jugadores.Find(jugadorID).EquipoID);
        }

        [HttpGet]
        public JsonResult ObtenerJugadoresLista(int equipoID)
        {
            //TODO: falta validar que no tenga multas.
            var jugadoresLista = _context.Jugadores.Where(x => x.EquipoID == equipoID &&
                                                               x.Calificado == true)
                                                        .Select(x => new { x.ID, Nombre = x.Nombre + " " + x.Apellido, x.Carnet });
            return Json(jugadoresLista);
        }

        [HttpGet]
        public JsonResult ObtenerFotoJugador(int jugadorID)
        {
            return Json(_context.Jugadores.Find(jugadorID).Foto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("EquipoID, EstadoCivilID, InstruccionID, ProfesionID, ProvinciaID, ParroquiaID, Cedula, Nombre, Apellido, FechaNacimiento, Carnet, FechaAfiliacion, NombreArchivo")] Jugadores jugadores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Jugadores entidad = new Jugadores
                {
                    EquipoID = jugadores.EquipoID == 0 ? null : jugadores.EquipoID,
                    EstadoCivilID = jugadores.EstadoCivilID,
                    InstruccionID = jugadores.InstruccionID,
                    ProfesionID = jugadores.ProfesionID,
                    ProvinciaID = jugadores.ProvinciaID,
                    ParroquiaID = jugadores.ParroquiaID,
                    Cedula = jugadores.Cedula.Trim(),
                    Nombre = jugadores.Nombre.Trim(),
                    Apellido = jugadores.Apellido.Trim(),
                    FechaNacimiento = jugadores.FechaNacimiento,
                    Carnet = jugadores.Carnet,
                    FechaAfiliacion = jugadores.FechaAfiliacion,
                    Foto = string.IsNullOrWhiteSpace(jugadores.NombreArchivo) == false ? FormateadorImagen.CambiarTamanio(path + "\\" + jugadores.NombreArchivo, 100, 200) : null,

                    //Valores fijos
                    Calificado = false,
                    Estado = true
                };

                _context.Jugadores.Add(entidad);
                await _context.SaveChangesAsync();

                // Al final de la modificación eliminamos el archivo
                if (!string.IsNullOrWhiteSpace(jugadores.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + jugadores.NombreArchivo);
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

            return Ok(jugadores);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("EquipoID, EstadoCivilID, InstruccionID, ProfesionID, ProvinciaID, ParroquiaID, Cedula, Nombre, Apellido, FechaNacimiento, Carnet, FechaAfiliacion, NombreArchivo")] Jugadores jugadores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Jugadores entidad = _context.Jugadores.Find(id);

                entidad.EquipoID = jugadores.EquipoID == 0 ? null : jugadores.EquipoID;
                entidad.EstadoCivilID = jugadores.EstadoCivilID;
                entidad.InstruccionID = jugadores.InstruccionID;
                entidad.ProfesionID = jugadores.ProfesionID;
                entidad.ProvinciaID = jugadores.ProvinciaID;
                entidad.ParroquiaID = jugadores.ParroquiaID;
                entidad.Cedula = jugadores.Cedula.Trim();
                entidad.Nombre = jugadores.Nombre.Trim();
                entidad.Apellido = jugadores.Apellido.Trim();
                entidad.FechaNacimiento = jugadores.FechaNacimiento;
                entidad.Carnet = jugadores.Carnet;
                entidad.FechaAfiliacion = jugadores.FechaAfiliacion;

                //Cada vez que se modifique algo sera enviado a calificar.
                entidad.Calificado = false;

                if (!string.IsNullOrWhiteSpace(jugadores.NombreArchivo))
                {
                    if (System.IO.File.Exists(path + "\\" + jugadores.NombreArchivo))
                    {
                        entidad.Foto = FormateadorImagen.CambiarTamanio(path + "\\" + jugadores.NombreArchivo, 100, 200);
                    }
                }

                _context.Jugadores.Update(entidad);
                await _context.SaveChangesAsync();

                // Al final de la modificación eliminamos el archivo
                if (!string.IsNullOrWhiteSpace(jugadores.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + jugadores.NombreArchivo);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calificar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Jugadores entidad = _context.Jugadores.Find(id);

                entidad.Calificado = true;

                _context.Jugadores.Update(entidad);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarEquipo(int equipoID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var entidades = _context.Jugadores.Where(x => x.EquipoID == null);
                foreach (var item in entidades)
                {
                    item.EquipoID = equipoID;
                }

                _context.Jugadores.UpdateRange(entidades);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id, bool nuevoEquipo)
        {
            try
            {
                if (nuevoEquipo)
                {
                    Jugadores entidad = _context.Jugadores.Find(id);
                    _context.Jugadores.Remove(entidad);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Jugadores entidad = _context.Jugadores.Find(id);

                    entidad.Estado = !entidad.Estado;

                    _context.Jugadores.Update(entidad);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> SubirFoto(IFormFile archivo)
        {
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";
            string nombre = DateTime.Now.ToString("HH.mm.ss.ffff.") + archivo.FileName;
            path = path + "\\" + nombre;

            if (archivo.Length > 0)
            {
                using (var stream = System.IO.File.Create(path))
                {
                    await archivo.CopyToAsync(stream);
                }
            }
            return Ok(new { foto = nombre });
        }

        [HttpPost]
        public IActionResult ElimintarFoto(string archivo)
        {
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";
            if (System.IO.File.Exists(path + "\\" + archivo))
            {
                System.IO.File.Delete(path + "\\" + archivo);
            }
            return Ok();
        }
    }
}