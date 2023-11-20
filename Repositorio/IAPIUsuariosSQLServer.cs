using APISED.Modelo;

namespace APISED.Repositorio
{
    public interface IAPIUsuariosSQLServer
    {
        Task<UsuarioAPI> DameUsuarioAPI(LoginAPI login);
    }
}
