
using System;

namespace SistemaContable
{
    public static class EstadoResultados
    {
        public static void Mostrar()
        {
            decimal Ingresos = Saldos.PorTipo("Ingreso");
            decimal Costos = Saldos.PorTipo("Costo");
            decimal Gastos = Saldos.PorTipo("Gasto");
            decimal UtilidadBruta = Ingresos - Costos;
            decimal UtilidadNeta = UtilidadBruta - Gastos;

            Console.WriteLine("\n=================================");
            Console.WriteLine("      ESTADO DE RESULTADOS       ");
            Console.WriteLine("=================================");
            
            Console.WriteLine("INGRESOS");
            Saldos.ListarPorTipo("Ingreso");
            Console.WriteLine("Total Ingresos: " + Ingresos.ToString("0.00"));

            Console.WriteLine("\nCOSTOS");
            Saldos.ListarPorTipo("Costo");
            Console.WriteLine("Total Costos: " + Costos.ToString("0.00"));

            Console.WriteLine("\nUtilidad bruta: " + UtilidadBruta.ToString("0.00"));

            Console.WriteLine("\nGASTOS");
            Saldos.ListarPorTipo("Gasto");
            Console.WriteLine("Total Gastos: " + Gastos.ToString("0.00"));

            string Texto = "UTILIDAD NETA: ";
            if (UtilidadNeta < 0)
            {
                Texto = "PERDIDA NETA: ";
            }

            Console.WriteLine("\n" + Texto + UtilidadNeta.ToString("0.00"));
            Console.WriteLine("=================================");
        }
    }
}


