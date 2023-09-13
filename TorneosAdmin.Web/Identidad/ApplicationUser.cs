using Microsoft.AspNetCore.Identity;
using System;

namespace TorneosAdmin.Web.Identidad
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser() { }

        public ApplicationUser(string userName) { UserName = userName; }

        public override int Id { get; set; }
        public override string UserName { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Password { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public bool PrimerInicio { get; set; }
        public bool Bloqueo { get; set; }
        public DateTimeOffset FechaBloqueo { get; set; }
        public int Intentos { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        //{
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    return userIdentity;
        //}

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, string authenticationType)
        //{
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
        //    return userIdentity;
        //}
    }
}
