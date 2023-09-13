using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TorneosAdmin.Web.Controllers
{
    [AllowAnonymous]
    public class PortadaController : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }
    }
}