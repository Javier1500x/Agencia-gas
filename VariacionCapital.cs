
using System;

namespace SistemaContable
{
    public static class VariacionCapital
    {
        public static void Mostrar()
        {
            decimal CapitalSocial = Saldos.PorCuenta(3101, "Acreedora");
            decimal Acumuladas = Saldos.PorCuenta(3102, "Acreedora");
            decimal Utilidad = Saldos.UtilidadNeta();
            decimal Retiros = Saldos.PorCuenta(3103, "Deudora");
            decimal CapitalFinal = CapitalSocial + Acumuladas + Utilidad - Retiros;

            Console.WriteLine("\n=================================");
            Console.WriteLine(" ESTADO DE VARIACIONES EN CAPITAL");
            Console.WriteLine("=================================");
            Console.WriteLine("Capital social: " + CapitalSocial.ToString("0.00"));
            Console.WriteLine("Utilidades acumuladas: " + Acumuladas.ToString("0.00"));
            Console.WriteLine("(+) Utilidad del ejercicio: " + Utilidad.ToString("0.00"));
            Console.WriteLine("(-) Retiros del dueño: " + Retiros.ToString("0.00"));
            Console.WriteLine("---------------------------------");
            Console.WriteLine("CAPITAL FINAL: " + CapitalFinal.ToString("0.00"));
            Console.WriteLine("=================================");
        }
    }
}
