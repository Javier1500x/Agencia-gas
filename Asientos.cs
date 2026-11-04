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

            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("               MOVIMIENTO CONTABLE");
            Console.WriteLine("-----------------------------------------------");

            nuevo.CodigoCuenta = LeerCodigoCuenta();

            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("  1. Debe");
            Console.WriteLine("  2. Haber");
            Console.WriteLine("-----------------------------------------------");

            int lado = Entrada.LeerEntero("\nSeleccione una opcion: ", 1, 2);

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
            Console.Clear();

            Console.WriteLine("===============================================");
            Console.WriteLine("|             REGISTRO DE ASIENTOS            |");
            Console.WriteLine("===============================================");

            DateTime Fecha = Entrada.LeerFecha("\nFecha (dd/mm/aaaa): ");

            string Descripcion = Entrada.LeerTexto("\nDescripcion: ");

            ListaMovimientos Lista = new ListaMovimientos();

            int Otro = 1;

            while (Otro == 1)
            {
                Lista.Agregar(PedirMovimiento());

                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("  1. Agregar otro movimiento");
                Console.WriteLine("  2. Finalizar movimientos");
                Console.WriteLine("-----------------------------------------------");

                Otro = Entrada.LeerEntero("\nSeleccione una opcion: ", 1, 2);
            }

            decimal TotalDebe = SumarDebe(Lista);
            decimal TotalHaber = SumarHaber(Lista);

            Console.WriteLine("\n===============================================");
            Console.WriteLine("|              RESUMEN DEL ASIENTO            |");
            Console.WriteLine("===============================================");

            if (Lista.Cantidad < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("Se necesitan al menos 2 movimientos.");
                Console.WriteLine("-----------------------------------------------");
                Console.ResetColor();
            }
            else if (TotalDebe != TotalHaber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("       EL ASIENTO NO ESTA CUADRADO");
                Console.WriteLine("-----------------------------------------------");
                Console.ResetColor();

                Console.WriteLine("Total Debe:  " + TotalDebe);
                Console.WriteLine("Total Haber: " + TotalHaber);

                Console.WriteLine("-----------------------------------------------");
            }
            else if (AsientosBD.Guardar(Fecha, Descripcion, Lista))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("       ASIENTO GUARDADO EXITOSAMENTE");
                Console.WriteLine("-----------------------------------------------");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("     ERROR AL GUARDAR EL ASIENTO");
                Console.WriteLine("-----------------------------------------------");
                Console.ResetColor();
            }
        }
    }
}

