
using System;

namespace SistemaContable
{
    public static class BalanceGeneral
    {
        public static void Mostrar()
        {
            int Forma = Entrada.LeerEntero("\n1. Forma de reporte / 2. Forma de cuenta: ", 1, 2);

            if (Forma == 1)
            {
                MostrarReporte();
            }
            else
            {
                MostrarCuenta();
            }
        }

        static void MostrarReporte()
        {
            decimal Activo = Saldos.PorTipo("Activo");
            decimal Pasivo = Saldos.PorTipo("Pasivo");
            decimal Capital = Saldos.PorTipo("Capital");
            decimal Utilidad = Saldos.UtilidadNeta();
            decimal totalCapital = Capital + Utilidad;

            Console.WriteLine("\n===============================================");
            Console.WriteLine("              BALANCE GENERAL");
            Console.WriteLine("===============================================");

            Console.WriteLine("\nActivos");
            Console.WriteLine("--------");
            Saldos.ListarPorTipo("Activo");
            Console.WriteLine("Total Activos: " + Activo);

            Console.WriteLine("\nPasivos");
            Console.WriteLine("--------");
            Saldos.ListarPorTipo("Pasivo");
            Console.WriteLine("Total Pasivos: " + Pasivo);
            Console.WriteLine("===============================================");

            Console.WriteLine("\nCapital");
            Console.WriteLine("--------");
            Saldos.ListarPorTipo("Capital");
            Console.WriteLine("Utilidad del ejercicio: " + Utilidad);
            Console.WriteLine("Capital Total: " + totalCapital);
            Console.WriteLine("===============================================");

            Console.WriteLine("\nPasivo + Capital: " + (Pasivo + totalCapital));
            Console.WriteLine("===============================================");

            if (Activo == Pasivo + totalCapital)
            {
                Console.WriteLine("\nEl Balance Cuadra");
            }
            else
            {
                Console.WriteLine("\nEl Balance NO cuadra");
            }
        }

        static void MostrarCuenta()
        {
            decimal Activo = Saldos.PorTipo("Activo");
            decimal Pasivo = Saldos.PorTipo("Pasivo");
            decimal Capital = Saldos.PorTipo("Capital");
            decimal Utilidad = Saldos.UtilidadNeta();
            decimal totalCapital = Capital + Utilidad;

            Console.WriteLine("\n===============================================================");
            Console.WriteLine("                      BALANCE GENERAL");
            Console.WriteLine("===============================================================");

            Console.WriteLine("ACTIVOS                         PASIVOS");
            Console.WriteLine("-------------------------------|-------------------------------");

            Saldos.ListarPorTipo("Activo");

            Console.WriteLine("                               |");

            Saldos.ListarPorTipo("Pasivo");

            Console.WriteLine("                               | CAPITAL");
            Saldos.ListarPorTipo("Capital");

            Console.WriteLine("                               | Utilidad del ejercicio: " + Utilidad);
            Console.WriteLine("                               | Capital Total: " + totalCapital);

            Console.WriteLine("-------------------------------|-------------------------------");
            Console.WriteLine("Total Activos: " + Activo);
            Console.WriteLine("                               | Total Pasivo + Capital: " + (Pasivo + totalCapital));
            Console.WriteLine("===============================================================");

            if (Activo == Pasivo + totalCapital)
            {
                Console.WriteLine("\nEl Balance Cuadra");
            }
            else
            {
                Console.WriteLine("\nEl Balance NO cuadra");
            }
        }
    }
}

