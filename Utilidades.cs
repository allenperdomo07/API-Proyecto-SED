using APISED.DTO;
using APISED.Modelo;

namespace APISED
{
    public  static class Utilidades
    {
        public static UsuarioDTO convertirDTO(this Usuario o)
        {
            if (o != null)
            {
                return new UsuarioDTO()
                {
                    Nombre = o.Nombre,
                    profesion = o.profesion,
                    NumeroTel = o.NumeroTel,
                    SKU = o.SKU,
                };

            }

            return null;
        }

        public static APIUsuarioDTO convertirDTO(this UsuarioAPI u)
        {
            if (u != null)
            {
                return new APIUsuarioDTO()
                {
                    Token = u.Token,
                    Usuario = u.Usuario
                };

            }

            return null;
        }


    }
}

