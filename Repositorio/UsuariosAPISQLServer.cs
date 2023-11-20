using APISED.Modelo;
using System.Data.SqlClient;
using System.Data;

namespace APISED.Repositorio
{
    public class UsuariosAPISQLServer: IAPIUsuariosSQLServer
    {
        private string CadenaConexion;
        private readonly ILogger<UsuariosAPISQLServer> log;
        public UsuariosAPISQLServer(AccesoaDatos cadenaConexion, ILogger<UsuariosAPISQLServer> l)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = l;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task<UsuarioAPI> DameUsuarioAPI(LoginAPI login)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            UsuarioAPI u = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuarioAPIObtener";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@UsuarioAPI", SqlDbType.VarChar, 500).Value = login.usuarioAPI;
                Comm.Parameters.Add("@PassApi", SqlDbType.VarChar, 500).Value = login.passAPI;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                if (reader.Read())
                {
                    u = new UsuarioAPI
                    {
                        Usuario = reader["UsuarioApi"].ToString(),
                        Email = reader["EmailUsuario"].ToString(),
                    };

                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al obtener datos del usuario de login" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return u;

        }
    }
}
