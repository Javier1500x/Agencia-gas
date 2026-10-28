using System;

namespace SistemaContable
{
    public static class BalanceGeneral
    {
        public static void Mostrar()
        {
            Console.Clear();

            decimal Activo = Saldos.PorTipo("Activo");
            decimal Pasivo = Saldos.PorTipo("Pasivo");
            decimal Capital = Saldos.PorTipo("Capital");
            decimal Utilidad = Saldos.UtilidadNeta();
            decimal TotalCapital = Capital + Utilidad;
            decimal TotalPasivoCapital = Pasivo + TotalCapital;

            Console.WriteLine("===============================================");
            Console.WriteLine("              BALANCE GENERAL");
            Console.WriteLine("===============================================");

            Console.WriteLine("\nACTIVOS");
            Console.WriteLine("-----------------------------------------------");
            Saldos.ListarPorTipo("Activo");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Total Activos: " + Activo);

            Console.WriteLine("\nPASIVOS");
            Console.WriteLine("-----------------------------------------------");
            Saldos.ListarPorTipo("Pasivo");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Total Pasivos: " + Pasivo);

            Console.WriteLine("\nCAPITAL");
            Console.WriteLine("-----------------------------------------------");
            Saldos.ListarPorTipo("Capital");
            Console.WriteLine("Utilidad del ejercicio: " + Utilidad);
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Total Capital: " + TotalCapital);

            Console.WriteLine("\n===============================================");
            Console.WriteLine("Total Pasivo + Capital: " + TotalPasivoCapital);
            Console.WriteLine("===============================================");

            if (Activo == TotalPasivoCapital)
            {
                Console.WriteLine("\nEl Balance Cuadra");
            }
            else
            {
                Console.WriteLine("\nEl Balance NO cuadra");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}