using System;
using System.Data.Common;

namespace SistemaContable
{
    class Menu
    {
        static void Main(string[] args)
        {
            if (SistemaContable.BaseDatos.ProbarConexion())
            {
                Console.WriteLine("Conexión exitosa");
            }
            else
            {
                Console.WriteLine("No se pudo conectar");
                return;
            }
                
            bool Continuar = true;

            while (Continuar)
            {
                MostrarMenu();
                int Opcion = LeerOpcion(1, 7);

                switch (Opcion)
                {
                    case 1:
                        Catalogo.Menu();
                        break;

                    case 2:
                        Console.WriteLine("Aqui va Registrar asiento");
                        break;

                    case 3:
                        Console.WriteLine("Aqui va El libro Mayor");
                        break;

                    case 4:
                        Console.WriteLine("Aqui va El libro mayor");
                        break;

                    case 5:
                        Console.WriteLine("Aqui va La balansa de comprobacion");
                        break;

                    case 6:
                        Console.WriteLine("Aqui van los estados financieros");
                        break;

                    case 7:
                        Continuar = false;
                        break;
                }
            }
        }
        static void MostrarMenu()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("SISTEMA CONTABLE - AGENCIA DE GAS");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Catalogo de Cuentas");
            Console.WriteLine("2. Registrar Asiento");
            Console.WriteLine("3. Libro Diario");
            Console.WriteLine("4. Libro Mayor");
            Console.WriteLine("5. Balanza de Comprobacion");
            Console.WriteLine("6. Estados Financieros");
            Console.WriteLine("7. Salir");
            Console.WriteLine("=================================");
        }
        static int LeerOpcion(int Min, int Max)
        {
            int Opcion;

            Console.Write("Ingrese que operacion desea realizar (1-7): ");

            while (!int.TryParse(Console.ReadLine(), out Opcion) || Opcion < Min || Opcion > Max)
            {
                Console.Write("\nError: Ingrese un numero valido o que este dentro del rango: ");
            }

            return Opcion;
        }
        
    }
    
}
