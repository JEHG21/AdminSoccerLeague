using System.ComponentModel.DataAnnotations;

namespace TorneosAdmin.Web.Models
{
    public class UsuariosLogins : UsuariosLoginKey
    {
        public string ProviderDisplayName { get; set; }
        [Key]
        public int UserId { get; set; }
    }
    public class UsuariosLoginKey
    {
        [Key]
        public string LoginProvider;
        [Key]
        public string ProviderKey;
    }
}
