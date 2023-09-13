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
    public class EquiposController : Controller
    {
        private readonly ModelEntities _context;

        public EquiposController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerEquipos(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var equiposLista = _context.Equipos.Select(x => new
            {
                x.ID,
                x.LigaID,
                x.SerieID,
                x.CategoriaID,
                x.DirigenteID,
                x.Nombre,
                x.Color,
                x.FechaFundacion,
                x.Estado,
                x.Foto
            });
            int totalRecords = equiposLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                equiposLista = equiposLista.OrderByDescending(t => t.Nombre);
                equiposLista = equiposLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                equiposLista = equiposLista.OrderBy(t => t.Nombre);
                equiposLista = equiposLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = equiposLista
            };
            return Json(jsonData);
        }

        [HttpGet]
        public JsonResult ObtenerTodosFotos()
        {
            // Obtenemos los equipos que se han inscripto en el campeonato
            var equipos = from e in _context.Equipos
                          select new { e.ID, e.Nombre, e.Foto };

            return Json(equipos);
        }

        [HttpGet]
        public JsonResult ObtenerFotos(int campeonatoID, int categoriaID, int serieID)
        {
            // Obtenemos los equipos que se han inscripto en el campeonato
            var equipos =  from e in _context.Equipos
                           join i in _context.Inscripciones on e.ID equals i.EquipoID
                           where i.CampeonatoID == campeonatoID &&
                                 e.CategoriaID == categoriaID &&
                                 e.SerieID == serieID
                           select new { e.ID, e.Nombre, e.Foto };

            return Json(equipos);
        }

        [HttpGet]
        public JsonResult ObtenerFotoEquipo(int equipoID)
        {
            return Json(_context.Equipos.Find(equipoID).Foto);
        }

        [HttpGet]
        public JsonResult ObtenerEquiposVista()
        {
            var equiposLista = from e in _context.Equipos
                               join l in _context.Ligas on e.LigaID equals l.ID
                               join s in _context.Series on e.SerieID equals s.ID
                               join c in _context.Categorias on e.CategoriaID equals c.ID
                               join d in _context.Dirigentes on e.DirigenteID equals d.ID
                               select new
                               {
                                   e.ID,
                                   Liga = l.Nombre,
                                   Categoria = c.Nombre,
                                   Serie = s.Nombre,
                                   Dirigente = d.Nombre + " " + d.Apellido,
                                   NombreEquipo = e.Nombre,
                                   e.Color,
                                   Fundacion = e.FechaFundacion.ToString("dd/MM/yyyy"),
                                   Estado = e.Estado ? "ACTIVO" : "INACTIVO",
                                   e.Foto,
                                   Jugadores = _context.Jugadores.Where(x => x.EquipoID == e.ID).Count()
                               };
            return Json(equiposLista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("LigaID, SerieID, CategoriaID, DirigenteID, Nombre, Color, FechaFundacion, NombreArchivo")] Equipos equipos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Equipos entidad = new Equipos
                {
                    LigaID = equipos.LigaID == 0 ? null : equipos.LigaID,
                    SerieID = equipos.SerieID == 0 ? null : equipos.SerieID,
                    CategoriaID = equipos.CategoriaID == 0 ? null : equipos.CategoriaID,
                    DirigenteID = equipos.DirigenteID == 0 ? null : equipos.DirigenteID,
                    Nombre = equipos.Nombre.Trim(),
                    Color = equipos.Color.Trim(),
                    FechaFundacion = equipos.FechaFundacion,
                    Foto = string.IsNullOrWhiteSpace(equipos.NombreArchivo) == false ? FormateadorImagen.CambiarTamanio(path + "\\" + equipos.NombreArchivo, 200, 200) : null,

                    //Valores fijos
                    Estado = true
                };

                _context.Equipos.Add(entidad);
                await _context.SaveChangesAsync();

                // Al final de la modificación eliminamos el archivo
                if (!string.IsNullOrWhiteSpace(equipos.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + equipos.NombreArchivo);

                return Ok(entidad);
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
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("LigaID, SerieID, CategoriaID, DirigenteID, Nombre, Color, FechaFundacion, NombreArchivo")] Equipos equipos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Equipos entidad = _context.Equipos.Find(id);

                entidad.LigaID = equipos.LigaID == 0 ? null : equipos.LigaID;
                entidad.SerieID = equipos.SerieID == 0 ? null : equipos.SerieID;
                entidad.CategoriaID = equipos.CategoriaID == 0 ? null : equipos.CategoriaID;
                entidad.DirigenteID = equipos.DirigenteID == 0 ? null : equipos.DirigenteID;
                entidad.Nombre = equipos.Nombre.Trim();
                entidad.Color = equipos.Color.Trim();
                entidad.FechaFundacion = equipos.FechaFundacion;

                if (!string.IsNullOrWhiteSpace(equipos.NombreArchivo))
                {
                    if (System.IO.File.Exists(path + "\\" + equipos.NombreArchivo))
                    {
                        entidad.Foto = FormateadorImagen.CambiarTamanio(path + "\\" + equipos.NombreArchivo, 200, 200);
                    }
                }

                _context.Equipos.Update(entidad);
                await _context.SaveChangesAsync();

                // Al final de la modificación eliminamos el archivo
                if (!string.IsNullOrWhiteSpace(equipos.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + equipos.NombreArchivo);
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
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Equipos entidad = _context.Equipos.Find(id);

                entidad.Estado = !entidad.Estado;

                _context.Equipos.Update(entidad);
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