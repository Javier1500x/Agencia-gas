
using System;
using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    public static class FlujoEfectivo
    {
        public static void Mostrar()
        {
            decimal Operacion = 0;
            decimal Inversion = 0;
            decimal Financiamiento = 0;

            using (SqlConnection Conexion = new SqlConnection(BaseDatos.Cadena))
            {
                Conexion.Open();
                string Consulta = "SELECT C.ActividadFlujo, SUM(M.Haber - M.Debe) " +
                                  "FROM Movimientos M " +
                                  "INNER JOIN Cuentas C ON C.Codigo = M.CodigoCuenta " +
                                  "WHERE M.CodigoCuenta NOT IN (1101, 1102) AND M.IdAsiento IN " +
                                  "(SELECT IdAsiento FROM Movimientos WHERE CodigoCuenta IN (1101, 1102)) " +
                                  "GROUP BY C.ActividadFlujo";

                SqlCommand Comando = new SqlCommand(Consulta, Conexion);
                SqlDataReader Lector = Comando.ExecuteReader();

                while (Lector.Read())
                {
                    string Actividad = Lector[0].ToString();
                    decimal Valor = decimal.Parse(Lector[1].ToString());

                    if (Actividad == "Operacion")
                    {
                        Operacion = Valor;
                    }
                    else if (Actividad == "Inversion")
                    {
                        Inversion = Valor;
                    }
                    else if (Actividad == "Financiamiento")
                    {
                        Financiamiento = Valor;
                    }
                }
                Lector.Close();
            }

            MostrarFlujo(Operacion, Inversion, Financiamiento);
        }

        static void MostrarFlujo(decimal Operacion, decimal Inversion, decimal Financiamiento)
        {
            decimal Aumento = Operacion + Inversion + Financiamiento;
            decimal Caja = Saldos.PorCuenta(1101, "Deudora");
            decimal Bancos = Saldos.PorCuenta(1102, "Deudora");

            Console.WriteLine("\n=================================");
            Console.WriteLine("   ESTADO DE FLUJO DE EFECTIVO   ");
            Console.WriteLine("=================================");
            Console.WriteLine("Actividades de operacion: " + Operacion.ToString("0.00"));
            Console.WriteLine("Actividades de inversion: " + Inversion.ToString("0.00"));
            Console.WriteLine("Actividades de financiamiento: " + Financiamiento.ToString("0.00"));
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Aumento neto de efectivo: " + Aumento.ToString("0.00"));
            Console.WriteLine("Caja + Bancos: " + (Caja + Bancos).ToString("0.00"));

            if (Aumento == (Caja + Bancos))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("El flujo cuadra con Caja y Bancos.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El flujo NO cuadra.");
                Console.ResetColor();
            }
            Console.WriteLine("=================================");
        }
    }
}


