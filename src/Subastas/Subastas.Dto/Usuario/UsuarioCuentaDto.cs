using Subastas.Domain;

namespace Subastas.Dto
{
    public class UsuarioCuentaViewModel
    {
        public Usuario Usuario { get; set; } = new Usuario();
        public Cuenta Cuenta { get; set; } = new Cuenta();
    }
}
