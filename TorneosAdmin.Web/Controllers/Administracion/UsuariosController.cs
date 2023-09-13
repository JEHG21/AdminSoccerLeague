using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ModelEntities _context;

        public UsuariosController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerUsuarios(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            // Nunca sera mostrado el usuario administrador para su edición
            var usuariosLista = _context.Usuarios.Where(x => x.ID != 1).Select(x => new
            {
                x.ID,
                x.Usuario,
                x.Nombre,
                x.ApellidoPaterno,
                x.ApellidoMaterno,
                x.CorreoElectronico,
                x.Telefono,
                x.Estado
            });
            int totalRecords = usuariosLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                usuariosLista = usuariosLista.OrderByDescending(t => t.Nombre);
                usuariosLista = usuariosLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                usuariosLista = usuariosLista.OrderBy(t => t.Nombre);
                usuariosLista = usuariosLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = usuariosLista
            };
            return Json(jsonData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Usuario, Nombre, ApellidoPaterno, ApellidoMaterno, CorreoElectronico, Telefono")] Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string usuario = CrearUsuario(usuarios.Nombre, usuarios.ApellidoPaterno);

                Usuarios entidad = new Usuarios
                {
                    Usuario = usuario,
                    Contrasena = FormateadorCadenas.HashedContraseña(usuario),
                    Nombre = usuarios.Nombre,
                    ApellidoPaterno = usuarios.ApellidoPaterno,
                    ApellidoMaterno = usuarios.ApellidoMaterno,
                    CorreoElectronico = usuarios.CorreoElectronico,
                    Telefono = usuarios.Telefono,

                    //Valores fijos
                    Bloqueo = false,
                    PrimerInicio = true,
                    Intentos = 0,
                    Estado = true,
                };

                _context.Usuarios.Add(entidad);
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

            return Ok(usuarios);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Usuario, Nombre, ApellidoPaterno, ApellidoMaterno, CorreoElectronico, Telefono")] Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (UsuarioExiste(id))
                {
                    Usuarios entidad = _context.Usuarios.Find(id);

                    entidad.Nombre = usuarios.Nombre.Trim();
                    entidad.ApellidoPaterno = usuarios.ApellidoPaterno.Trim();
                    entidad.ApellidoMaterno = usuarios.ApellidoMaterno.Trim();
                    entidad.CorreoElectronico = usuarios.CorreoElectronico.Trim();
                    entidad.Telefono = usuarios.Telefono;

                    _context.Usuarios.Update(entidad);
                    await _context.SaveChangesAsync();
                }
                else
                    return BadRequest("No existe el usuario a modificar.");
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
                if (UsuarioExiste(id))
                {
                    Usuarios entidad = _context.Usuarios.Find(id);

                    entidad.Estado = !entidad.Estado;

                    _context.Usuarios.Update(entidad);
                    await _context.SaveChangesAsync();
                }
                else
                    return BadRequest("No existe el usuario a eliminar.");
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
        private bool UsuarioExiste(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }

        [NonAction]
        private string CrearUsuario(string nombre, string apellidoPaterno)
        {
            string u = nombre.ToCharArray().ElementAt(0).ToString().ToLower() + apellidoPaterno.ToLower();

            for (int i = 1; i < 100; i++)
            {
                if (!_context.Usuarios.Any(e => e.Usuario == u))
                    break;
                else
                    u = u + i.ToString();
            }
            return u;
        }
    }
}