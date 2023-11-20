using APISED.Modelo;
using System.ComponentModel.DataAnnotations;

namespace APISED.Repositorio
{
    public class Usuariosenmemoria: IUsuariosenmemoria
    {
        private readonly List<Usuario> usuarios = new()
        {
            new Usuario {Id = 1, Nombre = "Allen Perdomo", profesion = "Estudiante", Direccion = "LA CIMA, SAN SALVADOR", email = "allenperdomo940@gmail.com", NumeroTel = 76682521, SKU = "00189021" },
            new Usuario {Id = 2, Nombre = "Pedro Gutierrez ", profesion = "Cocinero", Direccion = "SAN MARCOS, SAN SALVADOR", email = "popeye77@gmail.com", NumeroTel = 79549313, SKU = "00569322" },
            new Usuario {Id = 3, Nombre = "David Beckan", profesion = "Futbolista", Direccion = "SANTA TECLA, LA LIBERTAD", email = "beckandavid23@gmail.com", NumeroTel = 77457628, SKU = "00982220" }
        };


        public IEnumerable<Usuario> DameUsuarios()
        {
            return usuarios;
        }

        public Usuario DameUsuario(string SKU)
        {
            return usuarios.Where(p => p.SKU == SKU).SingleOrDefault();
        }

        public void CrearUsuario(Usuario o)
        {
            usuarios.Add(o);
        }

        public void ModificarUsuario(Usuario o)
        {
            int indice = usuarios.FindIndex(existeUsuario => existeUsuario.Id == o.Id);
            usuarios[indice] = o;
        }

        public void BorrarUsuario(string SKU)
        {
            int indice = usuarios.FindIndex(existeProducto => existeProducto.SKU == SKU);
            usuarios.RemoveAt(indice);
        }


    }
}
