using System;
using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    public static class Saldos
    {
        static void LeerTotales(string Consulta, string Valor, out decimal Debe, out decimal Haber)
        {
            Debe = 0;
            Haber = 0;

            using (SqlConnection Conexion = new SqlConnection(BaseDatos.Cadena))
            {
                Conexion.Open();
                SqlCommand Comando = new SqlCommand(Consulta, Conexion);
                Comando.Parameters.AddWithValue("@Valor", Valor);
                SqlDataReader Lector = Comando.ExecuteReader();

                if (Lector.Read())
                {
                    Debe = decimal.Parse(Lector[0].ToString());
                    Haber = decimal.Parse(Lector[1].ToString());
                }
                Lector.Close();
            }
        }

        public static decimal PorTipo(string Tipo)
        {
            decimal Debe, Haber;
            string Consulta = "SELECT ISNULL(SUM(M.Debe), 0), ISNULL(SUM(M.Haber), 0) " +
                              "FROM Movimientos M " +
                              "INNER JOIN Cuentas C ON C.Codigo = M.CodigoCuenta " +
                              "WHERE C.Tipo = @Valor";

            LeerTotales(Consulta, Tipo, out Debe, out Haber);

            if (EsAcreedor(Tipo))
            {
                return Haber - Debe;
            }
            return Debe - Haber;
        }

        public static decimal PorCuenta(int Codigo, string Naturaleza)
        {
            decimal Debe, Haber;
            string Consulta = "SELECT ISNULL(SUM(Debe), 0), ISNULL(SUM(Haber), 0) " +
                              "FROM Movimientos WHERE CodigoCuenta = @Valor";

            LeerTotales(Consulta, Codigo.ToString(), out Debe, out Haber);

            if (Naturaleza == "Acreedora")
            {
                return Haber - Debe;
            }
            return Debe - Haber;
        }

        public static decimal UtilidadNeta()
        {
            return PorTipo("Ingreso") - PorTipo("Costo") - PorTipo("Gasto");
        }

        static bool EsAcreedor(string Tipo)
        {
            if (Tipo == "Pasivo" || Tipo == "Capital" || Tipo == "Ingreso")
            {
                return true;
            }
            return false;
        }

        public static void ListarPorTipo(string Tipo)
        {
            using (SqlConnection Conexion = new SqlConnection(BaseDatos.Cadena))
            {
                Conexion.Open();
                string Consulta = "SELECT C.Nombre, ISNULL(SUM(M.Debe), 0), ISNULL(SUM(M.Haber), 0) " +
                                  "FROM Cuentas C " +
                                  "LEFT JOIN Movimientos M ON M.CodigoCuenta = C.Codigo " +
                                  "WHERE C.Tipo = @Tipo " +
                                  "GROUP BY C.Codigo, C.Nombre " +
                                  "ORDER BY C.Codigo";

                SqlCommand Comando = new SqlCommand(Consulta, Conexion);
                Comando.Parameters.AddWithValue("@Tipo", Tipo);
                SqlDataReader Lector = Comando.ExecuteReader();

                while (Lector.Read())
                {
                    decimal Debe = decimal.Parse(Lector[1].ToString());
                    decimal Haber = decimal.Parse(Lector[2].ToString());
                    decimal Saldo = 0;

                    if (EsAcreedor(Tipo))
                    {
                        Saldo = Haber - Debe;
                    }
                    else
                    {
                        Saldo = Debe - Haber;
                    }

                    if (Saldo != 0)
                    {
                        Console.WriteLine("  " + Lector[0] + ": " + Saldo.ToString("0.00"));
                    }
                }
                Lector.Close();
            }
        }
    }
}

