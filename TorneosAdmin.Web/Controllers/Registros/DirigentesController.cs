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
    public class DirigentesController : Controller
    {
        private readonly ModelEntities _context;

        public DirigentesController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerDirigentes(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var dirigentesLista = _context.Dirigentes.Select(x => new
            {
                x.ID,
                x.Cedula,
                x.Nombre,
                x.Apellido,
                x.Direccion,
                x.Telefono,
                x.Estado,
                x.Foto
            });
            int totalRecords = dirigentesLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                dirigentesLista = dirigentesLista.OrderByDescending(t => t.Nombre);
                dirigentesLista = dirigentesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                dirigentesLista = dirigentesLista.OrderBy(t => t.Nombre);
                dirigentesLista = dirigentesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = dirigentesLista
            };
            return Json(jsonData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Cedula, Nombre, Apellido, Direccion,  Telefono, NombreArchivo")] Dirigentes dirigentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Dirigentes entidad = new Dirigentes
                {
                    Cedula = dirigentes.Cedula.Trim(),
                    Nombre = dirigentes.Nombre.Trim(),
                    Apellido = dirigentes.Apellido.Trim(),
                    Direccion = dirigentes.Direccion.Trim(),
                    Telefono = dirigentes.Telefono,
                    Foto = string.IsNullOrWhiteSpace(dirigentes.NombreArchivo) == false ? FormateadorImagen.CambiarTamanio(path + "\\" + dirigentes.NombreArchivo, 275, 350) : null,

                    //Valores fijos
                    Estado = true
                };

                _context.Dirigentes.Add(entidad);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(dirigentes.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + dirigentes.NombreArchivo);
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

            return Ok(dirigentes);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Cedula, Nombre, Apellido, Direccion,  Telefono, NombreArchivo")] Dirigentes dirigentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Dirigentes entidad = _context.Dirigentes.Find(id);

                entidad.Cedula = dirigentes.Cedula.Trim();
                entidad.Nombre = dirigentes.Nombre.Trim();
                entidad.Apellido = dirigentes.Apellido.Trim();
                entidad.Direccion = dirigentes.Direccion.Trim();
                entidad.Telefono = dirigentes.Telefono;
                if (!string.IsNullOrWhiteSpace(dirigentes.NombreArchivo))
                {
                    if (System.IO.File.Exists(path + "\\" + dirigentes.NombreArchivo))
                    {
                        entidad.Foto = FormateadorImagen.CambiarTamanio(path + "\\" + dirigentes.NombreArchivo, 275, 350);
                    }
                }
                _context.Dirigentes.Update(entidad);
                await _context.SaveChangesAsync();

                // Al final de la modificación eliminamos el archivo
                if (!string.IsNullOrWhiteSpace(dirigentes.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + dirigentes.NombreArchivo);
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
                Dirigentes entidad = _context.Dirigentes.Find(id);

                entidad.Estado = !entidad.Estado;

                _context.Dirigentes.Update(entidad);
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