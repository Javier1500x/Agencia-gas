using System;
using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    public static class Balanza
    {
        public static void Mostrar()
        {
            decimal SumaDebe = 0;
            decimal SumaHaber = 0;

            using (SqlConnection Conexion = new SqlConnection(BaseDatos.Cadena))
            {
                try
                {
                    Conexion.Open();
                    string Consulta = "SELECT C.Codigo, C.Nombre, " +
                                      "SUM(M.Debe) AS TotalDebe, " +
                                      "SUM(M.Haber) AS TotalHaber " +
                                      "FROM Cuentas C " +
                                      "INNER JOIN Movimientos M ON M.CodigoCuenta = C.Codigo " +
                                      "GROUP BY C.Codigo, C.Nombre " +
                                      "ORDER BY C.Codigo";

                    SqlCommand Comando = new SqlCommand(Consulta, Conexion);
                    SqlDataReader Lector = Comando.ExecuteReader();

                    Console.WriteLine("\nCodigo | Nombre                | Debe       | Haber      | Saldo");
                    Console.WriteLine(new string('-', 75));

                    while (Lector.Read())
                    {
                        decimal Debe = Convert.ToDecimal(Lector["TotalDebe"]);
                        decimal Haber = Convert.ToDecimal(Lector["TotalHaber"]);

                        SumaDebe = SumaDebe + Debe;
                        SumaHaber = SumaHaber + Haber;

                        Console.WriteLine(Lector["Codigo"] + " | " +
                                          Lector["Nombre"].ToString().PadRight(21) + " | " +
                                          Debe.ToString("0.00") + " | " +
                                          Haber.ToString("0.00") + " | " +
                                          (Debe - Haber).ToString("0.00"));
                    }
                    Lector.Close();
                }
                catch (SqlException Error)
                {
                    Console.WriteLine("\nError en la balanza: " + Error.Message);
                }
            }

            MostrarTotales(SumaDebe, SumaHaber);
        }

        static void MostrarTotales(decimal SumaDebe, decimal SumaHaber)
        {
            Console.WriteLine(new string('-', 75));
            Console.WriteLine("Total Debe:  " + SumaDebe.ToString("0.00"));
            Console.WriteLine("Total Haber: " + SumaHaber.ToString("0.00"));
            
            if (SumaDebe == SumaHaber)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("La balanza cuadra.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La balanza NO cuadra.");
                Console.ResetColor();
            }
        }
    }
}


