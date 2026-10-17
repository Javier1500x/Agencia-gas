using System;
using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    public static class Catalogo
    {
        public static void Menu()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("         CATÁLOGO DE CUENTAS             ");
                Console.WriteLine("=========================================");
                Console.ResetColor();
                
                Console.WriteLine("1. Listar cuentas");
                Console.WriteLine("2. Agregar cuenta");
                Console.WriteLine("3. Modificar cuenta");
                Console.WriteLine("0. Volver al menú principal");
                
             
                Console.WriteLine("=========================================");
                Console.ResetColor();
                
                opcion = Entrada.LeerEntero("Seleccione una opción: ", 0, 3);

                switch (opcion)
                {
                    case 1:
                        Listar();
                        break;
                    case 2:
                        Agregar();
                        break;
                    case 3:
                        Modificar();
                        break;
                    case 0:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción inválida.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 0);
        }

        public static void Listar()
        {
            Console.Clear();
          Console.WriteLine("================================================================================");
            Console.WriteLine("                              LISTADO DE CUENTAS ")              ;
         Console.WriteLine("================================================================================");
            Console.ResetColor();

            using (SqlConnection conn = new SqlConnection(BaseDatos.Cadena))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Codigo, Nombre, Tipo, Naturaleza, ActividadFlujo FROM Cuentas ORDER BY Codigo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0,-8} | {1,-25} | {2,-10} | {3,-10} | {4,-15}", "Código", "Nombre", "Tipo", "Naturaleza", "Flujo");
                    Console.WriteLine(new string('-', 78));
                    Console.ResetColor();

                    while (reader.Read())
                    {
                        int codigo = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        string tipo = reader.GetString(2);
                        string naturaleza = reader.GetString(3);
                        string flujo = reader.GetString(4);

                        Console.WriteLine("{0,-8} | {1,-25} | {2,-10} | {3,-10} | {4,-15}", codigo, nombre, tipo, naturaleza, flujo);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error al listar las cuentas: " + ex.Message);
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPresione una tecla para regresar...");
            Console.ReadKey();
        }

        public static void Agregar()
        {
            Console.Clear();
           Console.WriteLine("===============================================");
            Console.WriteLine("           AGREGAR NUEVA CUENTA"    )              ;
            Console.WriteLine("===============================================");
        

            int codigo = Entrada.LeerEntero("Ingrese el código de la cuenta (ej. 1109): ", 1, 99999);

            if (ExisteCuenta(codigo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Ya existe una cuenta registrada con ese código.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            string nombre = Entrada.LeerTexto("Ingrese el nombre de la cuenta: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSeleccione el Tipo de Cuenta:");
            Console.ResetColor();
            Console.WriteLine("1. Activo");
            Console.WriteLine("2. Pasivo");
            Console.WriteLine("3. Capital");
            Console.WriteLine("4. Ingreso");
            Console.WriteLine("5. Costo");
            Console.WriteLine("6. Gasto");
            int optTipo = Entrada.LeerEntero("Opción (1-6): ", 1, 6);
            string tipo = optTipo switch
            {
                1 => "Activo",
                2 => "Pasivo",
                3 => "Capital",
                4 => "Ingreso",
                5 => "Costo",
                6 => "Gasto",
                _ => "Activo"
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSeleccione la Naturaleza:");
            Console.ResetColor();
            Console.WriteLine("1. Deudora");
            Console.WriteLine("2. Acreedora");
            int optNat = Entrada.LeerEntero("Opción (1-2): ", 1, 2);
            string naturaleza = optNat == 1 ? "Deudora" : "Acreedora";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSeleccione la Actividad de Flujo:");
            Console.ResetColor();
            Console.WriteLine("1. Operacion");
            Console.WriteLine("2. Inversion");
            Console.WriteLine("3. Financiamiento");
            Console.WriteLine("4. Ninguna");
            int optFlujo = Entrada.LeerEntero("Opción (1-4): ", 1, 4);
            string actividadFlujo = optFlujo switch
            {
                1 => "Operacion",
                2 => "Inversion",
                3 => "Financiamiento",
                4 => "Ninguna",
                _ => "Ninguna"
            };

            using (SqlConnection conn = new SqlConnection(BaseDatos.Cadena))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Cuentas (Codigo, Nombre, Tipo, Naturaleza, ActividadFlujo) VALUES (@Codigo, @Nombre, @Tipo, @Naturaleza, @ActividadFlujo)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@Naturaleza", naturaleza);
                    cmd.Parameters.AddWithValue("@ActividadFlujo", actividadFlujo);

                    cmd.ExecuteNonQuery();
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n¡Cuenta agregada exitosamente!");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error al agregar la cuenta: " + ex.Message);
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        public static void Modificar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- MODIFICAR CUENTA ---");
            Console.ResetColor();

            int codigo = Entrada.LeerEntero("Ingrese el código de la cuenta que desea modificar: ", 1, 99999);

            if (!ExisteCuenta(codigo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No existe ninguna cuenta con ese código.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            string nuevoNombre = Entrada.LeerTexto("Ingrese el nuevo nombre de la cuenta: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSeleccione el nuevo Tipo de Cuenta:");
            Console.ResetColor();
            Console.WriteLine("1. Activo");
            Console.WriteLine("2. Pasivo");
            Console.WriteLine("3. Capital");
            Console.WriteLine("4. Ingreso");
            Console.WriteLine("5. Costo");
            Console.WriteLine("6. Gasto");
            int optTipo = Entrada.LeerEntero("Opción (1-6): ", 1, 6);
            string nuevoTipo = optTipo switch
            {
                1 => "Activo",
                2 => "Pasivo",
                3 => "Capital",
                4 => "Ingreso",
                5 => "Costo",
                6 => "Gasto",
                _ => "Activo"
            };

            using (SqlConnection conn = new SqlConnection(BaseDatos.Cadena))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Cuentas SET Nombre = @Nombre, Tipo = @Tipo WHERE Codigo = @Codigo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Nombre", nuevoNombre);
                    cmd.Parameters.AddWithValue("@Tipo", nuevoTipo);

                    cmd.ExecuteNonQuery();
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n¡Cuenta modificada exitosamente!");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error al modificar la cuenta: " + ex.Message);
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        public static bool ExisteCuenta(int codigo)
        {
            bool existe = false;

            using (SqlConnection conn = new SqlConnection(BaseDatos.Cadena))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM Cuentas WHERE Codigo = @Codigo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        existe = true;
                    }
                }
                catch
                {
                    existe = false;
                }
            }

            return existe;
        }
    }
}