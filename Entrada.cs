using System;

namespace SistemaContable
{
    static class Entrada
    {
        public static int LeerEntero(string Mensaje, int Min, int Max)
        {
            int Numero;

            Console.Write(Mensaje);

            while (!int.TryParse(Console.ReadLine(), out Numero) || Numero < Min || Numero > Max)
            {
                Console.Write("\nError: Ingrese un numero valido o que este dentro del rando: ");
            }

            return Numero;
        }
        public static string LeerTexto(string Mensaje)
        {
            Console.Write(Mensaje);
            string Texto = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(Texto))
            {
                Console.Write("\nError: Ingrese algo: ");
                Texto = Console.ReadLine();
            }

            return Texto.Trim();
        }
        public static decimal LeerMonto(string Mensaje)
        {
            decimal Monto;

            Console.Write(Mensaje);

            while (!decimal.TryParse(Console.ReadLine(), out Monto) || Monto < 0)
            {
                Console.Write("\nError: Ingrese un numero valido positivo: ");
            }

            return Monto;
        }
        public static DateTime LeerFecha(string Mensaje)
        {
            DateTime Fecha;

            Console.Write(Mensaje);

            while (!DateTime.TryParse(Console.ReadLine(), out Fecha))
            {
                Console.Write("\nError: Ingrese la fecha correctamente (dd/mm/aaaa): ");
            }

            return Fecha;
        }
    }
}