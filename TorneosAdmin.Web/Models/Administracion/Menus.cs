using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneosAdmin.Web.Models
{
    public class Menus
    {
        [Column("MenuID")]
        public int ID { get; set; }

        public int MenusPadre { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Ruta { get; set; }

        public string Icono { get; set; }
    }
    public class MenuViewModel
    {
        public string Titulo { get; set; }
        public string Ruta { get; set; }
        public string Icono { get; set; }
        public IList<MenuViewModel> SubMenus { get; set; }
    }
}
