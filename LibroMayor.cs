using System;
using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    public static class LibroMayor
    {
        public static void Mostrar()
        {
            int Codigo = Asientos.LeerCodigoCuenta();
            string Naturaleza = ObtenerNaturaleza(Codigo);
            decimal Saldo = 0;

            using (SqlConnection Conexion = new SqlConnection(BaseDatos.Cadena))
            {
                try
                {
                    Conexion.Open();
                    string Consulta = "SELECT A.Fecha, A.Descripcion, M.Debe, M.Haber " +
                                      "FROM Movimientos M " +
                                      "INNER JOIN Asientos A ON A.Id = M.IdAsiento " +
                                      "WHERE M.CodigoCuenta = @Codigo " +
                                      "ORDER BY A.Fecha, A.Id, M.Id";

                    SqlCommand Comando = new SqlCommand(Consulta, Conexion);
                    Comando.Parameters.AddWithValue("@Codigo", Codigo);
                    SqlDataReader Lector = Comando.ExecuteReader();

                    Console.WriteLine("\nFecha       | Descripcion         | Debe       | Haber      | Saldo");
                    Console.WriteLine(new string('-', 75));

                    while (Lector.Read())
                    {
                        decimal Debe = Convert.ToDecimal(Lector["Debe"]);
                        decimal Haber = Convert.ToDecimal(Lector["Haber"]);
                        DateTime Dia = Convert.ToDateTime(Lector["Fecha"]);

                        Saldo = CalcularSaldo(Naturaleza, Saldo, Debe, Haber);

                        Console.WriteLine(Dia.ToShortDateString() + " | " +
                                          Lector["Descripcion"] + " | " +
                                          Debe.ToString("0.00") + " | " +
                                          Haber.ToString("0.00") + " | " +
                                          Saldo.ToString("0.00"));
                    }
                    Lector.Close();
                }
                catch (SqlException Error)
                {
                    Console.WriteLine("\nError en el libro mayor: " + Error.Message);
                }
            }
        }

        static string ObtenerNaturaleza(int Codigo)
        {
            string Naturaleza = "Deudora";

            using (SqlConnection Conexion = new SqlConnection(BaseDatos.Cadena))
            {
                try
                {
                    Conexion.Open();
                    string Consulta = "SELECT Naturaleza FROM Cuentas WHERE Codigo = @Codigo";
                    SqlCommand Comando = new SqlCommand(Consulta, Conexion);
                    Comando.Parameters.AddWithValue("@Codigo", Codigo);
                    
                    object resultado = Comando.ExecuteScalar();
                    if (resultado != null)
                    {
                        Naturaleza = resultado.ToString();
                    }
                }
                catch (SqlException Error)
                {
                    Console.WriteLine("\nError: " + Error.Message);
                }
            }

            return Naturaleza;
        }

        static decimal CalcularSaldo(string Naturaleza, decimal Saldo, decimal Debe, decimal Haber)
        {
            if (Naturaleza == "Deudora")
            {
                return Saldo + Debe - Haber;
            }
            return Saldo + Haber - Debe;
        }
    }
}