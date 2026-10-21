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

                    Console.WriteLine();
                 
                    Console.WriteLine("{0,-6} | {1,-21} | {2,10} | {3,10} | {4,10}", "Codigo", "Nombre", "Debe", "Haber", "Saldo");
                    Console.WriteLine(new string('-', 75));

                    while (Lector.Read())
                    {
                        decimal Debe = Convert.ToDecimal(Lector["TotalDebe"]);
                        decimal Haber = Convert.ToDecimal(Lector["TotalHaber"]);
                        decimal Saldo = Debe - Haber;

                        SumaDebe = SumaDebe + Debe;
                        SumaHaber = SumaHaber + Haber;

                        Console.WriteLine("{0,-6} | {1,-21} | {2,10} | {3,10} | {4,10}", 
                            Lector["Codigo"], 
                            Lector["Nombre"], 
                            Debe.ToString("0.00"), 
                            Haber.ToString("0.00"), 
                            Saldo.ToString("0.00"));
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