using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace SistemaContable
{
    static class Asientos
    {
         public static int LeerCodigoCuenta()
        {
            int Codigo;

            Console.Write("\nCodigo de cuenta: ");

            while (!int.TryParse(Console.ReadLine(), out Codigo) || !Catalogo.ExisteCuenta(Codigo))
            {
                Console.Write("\nError: Ingrese un codigo que exista: ");
            }

            return Codigo;
        }
        static decimal LeerMontoPositivo()
        {
            decimal Monto = Entrada.LeerMonto("\nMonto: ");

            while (Monto == 0)
            {
                Console.Write("\nError: El Monto debe ser mayor que cero: ");
                Monto = Entrada.LeerMonto("\nMonto: ");
            }

            return Monto;
        }
        static Movimiento PedirMovimiento()
        {
            Movimiento nuevo = new Movimiento();

            nuevo.CodigoCuenta = LeerCodigoCuenta();
            int lado = Entrada.LeerEntero("\n1. Debe / 2. Haber: ", 1, 2);
            decimal Monto = LeerMontoPositivo();

            if (lado == 1)
            {
                nuevo.Debe = Monto;
            }
            else
            {
                nuevo.Haber = Monto;
            }

            return nuevo;
        }
        static decimal SumarDebe(ListaMovimientos Lista)
        {
            decimal Total = 0;
            Nodo Actual = Lista.Cabeza;

            while (Actual != null)
            {
                Total = Total + Actual.Dato.Debe;
                Actual = Actual.Siguiente;
            }

            return Total;
        }
        static decimal SumarHaber(ListaMovimientos Lista)
        {
            decimal Total = 0;
            Nodo Actual = Lista.Cabeza;

            while (Actual != null)
            {
                Total = Total + Actual.Dato.Haber;
                Actual = Actual.Siguiente;
            }

            return Total;
        }
        public static void Registrar()
        {
            DateTime Fecha = Entrada.LeerFecha("\nFecha (dd/mm/aaaa): ");
            string Descripcion = Entrada.LeerTexto("\nDescripcion: ");
            ListaMovimientos Lista = new ListaMovimientos();
            int Otro = 1;

            while (Otro == 1)
            {
                Lista.Agregar(PedirMovimiento());
                Otro = Entrada.LeerEntero("\nAgregar Otro Movimiento? 1. Si / 2. No: ", 1, 2);
            } 

            decimal TotalDebe = SumarDebe(Lista);
            decimal TotalHaber = SumarHaber(Lista);

            if (Lista.Cantidad < 2)
            {
                Console.WriteLine("\nError: Se necesitan al menos 2 movimientos.");
            }
                else if (TotalDebe != TotalHaber)
            {
                Console.WriteLine("\nError: El asiento no cuadra.");
                Console.WriteLine("Total Debe: " + TotalDebe);
                Console.WriteLine("Total Haber: " + TotalHaber);
            }
            else if (AsientosBD.Guardar(Fecha, Descripcion, Lista))
            {
                Console.WriteLine("\nAsiento guardado exitosamente");
            }
            else
            {
                Console.WriteLine("\nError al intentar guardar el asiento.");
            }
          
        }
    }
}