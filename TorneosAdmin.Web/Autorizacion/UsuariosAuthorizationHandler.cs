using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Autorizacion
{
    public class UsuariosAuthorizationHandler : IAuthorizationHandler
    {
        private readonly ModelEntities _context;
        private readonly string[] controllers = new string[] { "Administracion", "Campeonato", "Catalogos", "registro", "Reportes", "Tesoreria" };
        public UsuariosAuthorizationHandler(ModelEntities context)
        {
            _context = context;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
            }

            if (!context.User.IsInRole("Administrador"))
            {
                var mvcContext = context.Resource as AuthorizationFilterContext;
                var descriptor = mvcContext?.ActionDescriptor as ControllerActionDescriptor;
                if (controllers.Contains(descriptor.ControllerName))
                {
                    var permisos = (from ur in _context.UsuariosRoles
                                    join p in _context.Permisos on ur.RolID equals p.RolID
                                    join m in _context.Menus on p.MenuID equals m.ID
                                    where ur.UsuarioID == Convert.ToInt32(context.User.Claims.First().Value) &&
                                          m.Ruta != ""
                                    select new
                                    {
                                        controlador = m.Ruta.Split("/", StringSplitOptions.None)[0],
                                        vista = m.Ruta.Split("/", StringSplitOptions.None)[1]
                                    }).ToList();
                    var permiso = permisos.Where(x => x.controlador == descriptor.ControllerName && x.vista == descriptor.ActionName);

                    if (permiso.Count() == 0)
                        context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
