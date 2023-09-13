using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Extensiones;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class CampeonatosController : Controller
    {
        private readonly ModelEntities _context;

        public CampeonatosController(ModelEntities context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult ObtenerCampeonatos(string si1dx, string sort, int page, int rows)
        {
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var rolesLista = _context.Campeonatos.Select(x => new
            {
                x.ID,
                x.Nombre,
                x.FechaInicio,
                x.FechaFin,
                x.Estado
            });
            int totalRecords = rolesLista.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sort.ToUpper() == "DESC")
            {
                rolesLista = rolesLista.OrderByDescending(t => t.Nombre);
                rolesLista = rolesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                rolesLista = rolesLista.OrderBy(t => t.Nombre);
                rolesLista = rolesLista.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = rolesLista
            };
            return Json(jsonData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Nombre, FechaInicio, FechaFin")] Campeonatos campeonatos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!CampeonatoExiste(campeonatos.Nombre.Trim()))
                {
                    if (campeonatos.FechaInicio < campeonatos.FechaFin)
                    {
                        campeonatos.Nombre = campeonatos.Nombre.Trim();

                        //Valores fijos
                        campeonatos.Estado = true;

                        _context.Campeonatos.Add(campeonatos);
                        await _context.SaveChangesAsync();
                    }
                    else
                        return BadRequest("La fecha de inicio no debe ser mayor a la fecha fin del campeonato, ingrese la fecha correcta.");
                }
                else
                    return BadRequest("El nombre del campeonato ya se encuentra registrado, ingrese otro nombre.");
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

            return Ok(campeonatos);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Nombre, FechaInicio, FechaFin")] Campeonatos campeonatos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Campeonatos entidad = _context.Campeonatos.Find(id);
                if (entidad.Nombre != campeonatos.Nombre.Trim())
                {
                    if (CampeonatoExiste(campeonatos.Nombre.Trim()))
                    {
                        return BadRequest("El nombre del campeonato ya se encuentra registrado, ingrese otro nombre.");
                    }
                }

                if (campeonatos.FechaInicio < campeonatos.FechaFin)
                {
                    entidad.Nombre = campeonatos.Nombre.Trim();
                    entidad.FechaInicio = campeonatos.FechaInicio;
                    entidad.FechaFin = campeonatos.FechaFin;
                    entidad.Estado = campeonatos.Estado;

                    _context.Campeonatos.Update(entidad);
                    await _context.SaveChangesAsync();
                }
                else
                    return BadRequest("La fecha de inicio no debe ser mayor a la fecha fin del campeonato, ingrese la fecha correcta.");

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
                Campeonatos entidad = _context.Campeonatos.Find(id);

                entidad.Estado = !entidad.Estado;

                _context.Campeonatos.Update(entidad);
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
        private bool CampeonatoExiste(string nombre)
        {
            return _context.Campeonatos.Any(e => e.Nombre == nombre);
        }
    }
}