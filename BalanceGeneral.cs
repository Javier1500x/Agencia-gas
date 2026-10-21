using System;

namespace SistemaContable
{
    public static class BalanceGeneral
    {
        public static void Mostrar()
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
            Saldos.ListarPorTipo("Pasivos");
            Console.WriteLine("Total Pasivos: " + Pasivo);
            Console.WriteLine("===============================================");

            Console.WriteLine("Capital");
            Console.WriteLine("--------");
            Saldos.ListarPorTipo("Capital");
            Console.WriteLine("Utilidad del ejercicio: " + Utilidad);
            Console.WriteLine("Capital Total: " + totalCapital);
            Console.WriteLine("===============================================");

            Console.WriteLine("\n===============================================");
            Console.WriteLine("Pasivo + Capital: " + (Pasivo + totalCapital));
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
        
    }
}