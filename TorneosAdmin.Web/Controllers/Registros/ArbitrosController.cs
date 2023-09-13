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
    public class ArbitrosController : Controller
    {
        private readonly ModelEntities _context;

        public ArbitrosController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerArbitros(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var arbitrosLista = _context.Arbitros.Select(x => new
            {
                x.ID,
                x.Cedula,
                x.Nombre,
                x.Apellido,
                x.Direccion,
                x.Pais,
                x.Telefono,
                x.CorreoElectronico,
                x.Estado,
                x.Foto
            });
            int totalRecords = arbitrosLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                arbitrosLista = arbitrosLista.OrderByDescending(t => t.Nombre);
                arbitrosLista = arbitrosLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                arbitrosLista = arbitrosLista.OrderBy(t => t.Nombre);
                arbitrosLista = arbitrosLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = arbitrosLista
            };
            return Json(jsonData);
        }

        [HttpGet]
        public JsonResult ObtenerFotoArbitro(int arbitroID)
        {
            return Json(_context.Arbitros.Find(arbitroID).Foto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Cedula, Nombre, Apellido, Direccion, Pais, Telefono, CorreoElectronico, NombreArchivo")] Arbitros arbitros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Arbitros entidad = new Arbitros
                {
                    Cedula = arbitros.Cedula.Trim(),
                    Nombre = arbitros.Nombre.Trim(),
                    Apellido = arbitros.Apellido.Trim(),
                    Direccion = arbitros.Direccion.Trim(),
                    Pais = arbitros.Pais.Trim(),
                    Telefono = arbitros.Telefono,
                    CorreoElectronico = arbitros.CorreoElectronico.Trim(),
                    Foto = string.IsNullOrWhiteSpace(arbitros.NombreArchivo) == false ? FormateadorImagen.CambiarTamanio(path + "\\" + arbitros.NombreArchivo, 275, 350) : null,

                    //Valores fijos
                    Estado = true
                };

                _context.Arbitros.Add(entidad);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(arbitros.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + arbitros.NombreArchivo);
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

            return Ok(arbitros);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Cedula, Nombre, Apellido, Direccion, Pais, Telefono, CorreoElectronico, NombreArchivo")] Arbitros arbitros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\TempFotos"}";

                Arbitros entidad = _context.Arbitros.Find(id);

                entidad.Cedula = arbitros.Cedula.Trim();
                entidad.Nombre = arbitros.Nombre.Trim();
                entidad.Apellido = arbitros.Apellido.Trim();
                entidad.Direccion = arbitros.Direccion.Trim();
                entidad.Pais = arbitros.Pais.Trim();
                entidad.Telefono = arbitros.Telefono;
                entidad.CorreoElectronico = arbitros.CorreoElectronico.Trim();

                if (!string.IsNullOrWhiteSpace(arbitros.NombreArchivo))
                {
                    if (System.IO.File.Exists(path + "\\" + arbitros.NombreArchivo))
                    {
                        entidad.Foto = FormateadorImagen.CambiarTamanio(path + "\\" + arbitros.NombreArchivo, 275, 350);
                    }
                }
                _context.Arbitros.Update(entidad);
                await _context.SaveChangesAsync();

                // Al final de la modificación eliminamos el archivo
                if (!string.IsNullOrWhiteSpace(arbitros.NombreArchivo))
                    System.IO.File.Delete(path + "\\" + arbitros.NombreArchivo);
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
                Arbitros entidad = _context.Arbitros.Find(id);

                entidad.Estado = !entidad.Estado;

                _context.Arbitros.Update(entidad);
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