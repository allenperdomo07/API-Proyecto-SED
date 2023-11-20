using APISED.Modelo;

namespace APISED.Repositorio
{
    public interface IUsuariosenmemoria
    {
        Usuario DameUsuario(string SKU);
       
        IEnumerable<Usuario> DameUsuarios();
        void CrearUsuario(Usuario o);
        void ModificarUsuario(Usuario o);
        void BorrarUsuario(string SKU);


    }
}
