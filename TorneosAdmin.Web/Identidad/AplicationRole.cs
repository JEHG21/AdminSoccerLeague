using Microsoft.AspNetCore.Identity;

namespace TorneosAdmin.Web.Identidad
{
    public class AplicationRole : IdentityRole<int>
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public bool IsDelete { get; set; }

        public AplicationRole()
        {
        }

        public AplicationRole(string name)
        {
            Name = name;
        }
    }
}
