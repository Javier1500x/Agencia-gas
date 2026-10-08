using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    static class BaseDatos
    {
        public const string Cadena =
            @"Server=localhost\SQLEXPRESS;Database=AgenciaGas;Trusted_Connection=True;TrustServerCertificate=True;";

        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Cadena))
                {
                    con.Open();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}