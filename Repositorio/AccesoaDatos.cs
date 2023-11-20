namespace APISED.Repositorio
{
    public class AccesoaDatos
    {
        private string cadenaConexionSql;
        public string CadenaConexionSQL { get => cadenaConexionSql; }
        public AccesoaDatos(string ConexionSql)
        {
            cadenaConexionSql = ConexionSql;
        }
    }
}
