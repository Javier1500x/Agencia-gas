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
                int Opcion = Entrada.LeerEntero("Ingrese que operacion desea realizar (1-7): ", 1, 7);

                switch (Opcion)
                {
                    case 1:
                        Catalogo.Menu();
                        break;

                    case 2:
                        Asientos.Registrar();
                        Console.ReadKey();
                        break;

                    case 3:
                        LibroDiario.Mostrar();
                        break;

                    case 4:
                        LibroMayor.Mostrar();
                        break;

                    case 5:
                        Balanza.Mostrar();
                        break;

                    case 6:
                        Estados.Menu();
                        break;

                    case 7:
                        Continuar = false;
                        break;
                }
            }
        }
        static void MostrarMenu()
        {
            Console.WriteLine("\n================================================");
            Console.WriteLine("       SISTEMA CONTABLE - AGENCIA DE GAS");
            Console.WriteLine("================================================");
            Console.WriteLine("1. Catalogo de Cuentas");
            Console.WriteLine("2. Registrar Asiento");
            Console.WriteLine("3. Libro Diario");
            Console.WriteLine("4. Libro Mayor");
            Console.WriteLine("5. Balanza de Comprobacion");
            Console.WriteLine("6. Estados Financieros");
            Console.WriteLine("7. Salir");
            Console.WriteLine("================================================");
        } 
    }
    
}
