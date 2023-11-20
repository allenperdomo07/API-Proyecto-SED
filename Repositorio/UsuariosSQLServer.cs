using APISED.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace APISED.Repositorio
{
    public class UsuariosSQLServer : IUsuariosenmemoria
    {
        private string CadenaConexion;

        public UsuariosSQLServer(AccesoaDatos cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public void BorrarUsuario(string SKU)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
      
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuariosBorrar";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                Comm.ExecuteNonQuery();

            
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al eliminar " + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            
        }

        public void CrearUsuario(Usuario o)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuariosSubida";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = o.Nombre;
                Comm.Parameters.Add("@profesion", SqlDbType.VarChar, 5000).Value = o.profesion;
                Comm.Parameters.Add("@NumeroTel", SqlDbType.Int).Value = o.NumeroTel;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = o.SKU;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al dar de alta" + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public Usuario DameUsuario(string SKU)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            Usuario o = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuariosObtener";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                SqlDataReader reader = Comm.ExecuteReader();

                if (reader.Read())
                {
                    o = new Usuario
                    {
                        Nombre = reader["Nombre"].ToString(),
                        profesion = reader["Profesion"].ToString(),
                        NumeroTel = Convert.ToInt32(reader["NumeroTel"].ToString()),
                        SKU = reader["SKU"].ToString()
                    };

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al dar de alta" + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return o;
        }

        public IEnumerable<Usuario> DameUsuarios()
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            List<Usuario> usuarios = new List<Usuario>();
            Usuario o = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuariosObtener";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Comm.ExecuteReader();

                while (reader.Read())
                {
                    o = new Usuario
                    {
                        Nombre = reader["Nombre"].ToString(),
                        profesion = reader["Profesion"].ToString(),
                        NumeroTel = Convert.ToInt32(reader["NumeroTel"].ToString()),
                        SKU = reader["SKU"].ToString()
                    };

                    usuarios.Add(o);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al dar de alta" + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return usuarios;
        }

        public void ModificarUsuario(Usuario o)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuariosModificar";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = o.Nombre;
                Comm.Parameters.Add("@profesion", SqlDbType.VarChar, 5000).Value = o.profesion;
                Comm.Parameters.Add("@NumeroTel", SqlDbType.Int).Value = o.NumeroTel;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = o.SKU;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al modificar el usuario" + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }
    }
}
