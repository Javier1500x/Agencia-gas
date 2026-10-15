using System;
using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    static class LibroDiario
    {
        static ListaAsientos CargarAsientos()
        {
            ListaAsientos Lista = new ListaAsientos();
            SqlConnection Conexion = new SqlConnection(BaseDatos.Cadena);
            Conexion.Open();

            string Consulta = "SELECT A.Id, A.Fecha, A.Descripcion, M.CodigoCuenta, C.Nombre, M.Debe, M.Haber " +
                              "FROM Asientos A " +
                              "INNER JOIN Movimientos M ON M.IdAsiento = A.Id " +
                              "INNER JOIN Cuentas C ON C.Codigo = M.CodigoCuenta " +
                              "ORDER BY A.Fecha, A.Id, M.Id";

            SqlCommand Comando = new SqlCommand(Consulta, Conexion);
            SqlDataReader Lector = Comando.ExecuteReader();
            NodoAsiento Actual = null;

            while (Lector.Read())
            {
                int Id = int.Parse(Lector["Id"].ToString());

                if (Actual == null || Actual.Id != Id)
                {
                    Actual = new NodoAsiento();
                    Actual.Id = Id;
                    Actual.Fecha = DateTime.Parse(Lector["Fecha"].ToString());
                    Actual.Descripcion = Lector["Descripcion"].ToString();
                    Actual.Movimientos = new ListaMovimientos();
                    Lista.Agregar(Actual);
                }

                Movimiento NuevoMovimiento = new Movimiento();
                NuevoMovimiento.CodigoCuenta = int.Parse(Lector["CodigoCuenta"].ToString());
                NuevoMovimiento.Nombre = Lector["Nombre"].ToString();
                NuevoMovimiento.Debe = decimal.Parse(Lector["Debe"].ToString());
                NuevoMovimiento.Haber = decimal.Parse(Lector["Haber"].ToString());
                Actual.Movimientos.Agregar(NuevoMovimiento);
            }

            Conexion.Close();
            return Lista;
        }

        static void MostrarAsiento(NodoAsiento Asiento)
        {
            Console.WriteLine("\n===============================================");
            Console.WriteLine("Asiento No. " + Asiento.Id);
            Console.WriteLine("Fecha: " + Asiento.Fecha.Day + "/" + Asiento.Fecha.Month + "/" + Asiento.Fecha.Year);
            Console.WriteLine("Descripcion: " + Asiento.Descripcion);
            Console.WriteLine("-----------------------------------------------");

            Nodo Actual = Asiento.Movimientos.Cabeza;

            while (Actual != null)
            {
                Console.WriteLine(Actual.Dato.CodigoCuenta + " " + Actual.Dato.Nombre + " | Debe: " + Actual.Dato.Debe + " | Haber: " + Actual.Dato.Haber);
                Actual = Actual.Siguiente;
            }
        }
        static NodoAsiento IrAlAnterior(NodoAsiento Actual)
        {
            if (Actual.Anterior == null)
            {
                Console.WriteLine("\nEste es el primer asiento.");
                return Actual;
            }

            return Actual.Anterior;
        }

        static NodoAsiento IrAlSiguiente(NodoAsiento Actual)
        {
            if (Actual.Siguiente == null)
            {
                Console.WriteLine("\nEste es el ultimo asiento.");
                return Actual;
            }

            return Actual.Siguiente;
        }
        public static void Mostrar()
        {
            ListaAsientos Lista = CargarAsientos();

            if (Lista.Cantidad == 0)
            {
                Console.WriteLine("\nNo hay asientos registrados.");
                return;
            }

            NodoAsiento Actual = Lista.Cabeza;
            int Opcion = 0;

            while (Opcion != 3)
            {
                MostrarAsiento(Actual);
                Opcion = Entrada.LeerEntero("\n1. Anterior / 2. Siguiente / 3. Volver: ", 1, 3);

                if (Opcion == 1)
                {
                    Actual = IrAlAnterior(Actual);
                }
                else if (Opcion == 2)
                {
                    Actual = IrAlSiguiente(Actual);
                }
            }
        }
    }
}