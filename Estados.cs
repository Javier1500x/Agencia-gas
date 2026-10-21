using System;
using System.Diagnostics;

namespace SistemaContable
{
    static class Estados
    {
        public static void Menu()
        {
            Console.Clear();

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine("\n===============================================");
                Console.WriteLine("              ESTADOS FINANCIEROS");
                Console.WriteLine("===============================================");
                Console.WriteLine("1. Estado de Resultado");
                Console.WriteLine("2. Variacion de Capital");
                Console.WriteLine("3. Balance General");
                Console.WriteLine("4. Flujo de Efectivo");
                Console.WriteLine("5. Volver");
                Console.WriteLine("===============================================");

                int Opcion = Entrada.LeerEntero("Eliga el Estado Financiero: ", 1, 5);

                switch (Opcion)
                {
                    case 1:
                        Console.Clear();
                        EstadoResultados.Mostrar();
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        VariacionCapital.Mostrar();
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        BalanceGeneral.Mostrar();
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.Clear();
                        FlujoEfectivo.Mostrar();
                        Console.ReadKey();
                        break;

                    case 5:
                        continuar = false;
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}