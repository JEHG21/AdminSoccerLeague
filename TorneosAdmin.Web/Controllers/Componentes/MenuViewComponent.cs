using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Controllers
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ModelEntities _db;
        public MenuViewComponent(ModelEntities db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(int usuarioID)
        {
            //Variables
            List<MenuViewModel> result = new List<MenuViewModel>();

            if (usuarioID == 1)
                result = CrearMenus(_db.Menus.ToList());
            else
            {
                // Filtramos los roles del usuario
                var roles = _db.UsuariosRoles.Where(x => x.UsuarioID == usuarioID).Select(x => x.RolID);

                // Filtramos los permisos de los roles
                var permisos = _db.Permisos.Where(x => roles.Contains(x.RolID)).Select(x => x.MenuID);

                result = CrearMenus(_db.Menus.Where(x => permisos.Contains(x.ID)).ToList());
            }
            return View(result);
        }

        private List<MenuViewModel> CrearMenus(List<Menus> menus)
        {
            var menu = new List<MenuViewModel>();
            foreach (var item in menus.Where(x => x.MenusPadre == 0))
            {
                var menuElemento = new MenuViewModel()
                {
                    Titulo = item.Titulo,
                    Ruta = item.Ruta,
                    Icono = item.Icono
                };

                if (menus.Where(x => x.MenusPadre == item.ID).Any())
                    menuElemento.SubMenus = CreaSubmenus(menus, item.ID);
                menu.Add(menuElemento);
            }
            return menu;
        }

        private List<MenuViewModel> CreaSubmenus(List<Menus> menus, int idPadre)
        {
            var menuElemento = new List<MenuViewModel>();
            foreach (var item in menus.Where(x => x.MenusPadre == idPadre))
            {
                var subMenu = new MenuViewModel()
                {
                    Titulo = item.Titulo,
                    Ruta = item.Ruta,
                    Icono = item.Icono
                };

                if (menus.Where(x => x.MenusPadre == item.ID).Any())
                    subMenu.SubMenus = CreaSubmenus(menus, item.ID);
                menuElemento.Add(subMenu);
            }
            return menuElemento;
        }
    }
}
