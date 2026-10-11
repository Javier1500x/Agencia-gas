using System;
using Microsoft.Data.SqlClient;

namespace SistemaContable
{
    public static class AsientosBD
    {
        public static bool Guardar(DateTime fecha, string descripcion, ListaMovimientos lista)
        {
            using (SqlConnection conexion = new SqlConnection(BaseDatos.Cadena))
            {
                SqlTransaction transaccion = null;
                try
                {
                    conexion.Open();
                    transaccion = conexion.BeginTransaction();

                    string queryCabecera = "INSERT INTO Asientos (Fecha, Descripcion) VALUES (@Fecha, @Descripcion); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmdCabecera = new SqlCommand(queryCabecera, conexion, transaccion))
                    {
                        cmdCabecera.Parameters.AddWithValue("@Fecha", fecha);
                        cmdCabecera.Parameters.AddWithValue("@Descripcion", descripcion);

                        int idAsiento = Convert.ToInt32(cmdCabecera.ExecuteScalar());

                        Nodo actual = lista.Cabeza;
                        while (actual != null)
                        {
                            Movimiento m = actual.Dato;
                            string queryDetalle = "INSERT INTO Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) VALUES (@IdAsiento, @CodigoCuenta, @Debe, @Haber)";
                            
                            using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conexion, transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue("@IdAsiento", idAsiento);
                                cmdDetalle.Parameters.AddWithValue("@CodigoCuenta", m.CodigoCuenta);
                                cmdDetalle.Parameters.AddWithValue("@Debe", m.Debe);
                                cmdDetalle.Parameters.AddWithValue("@Haber", m.Haber);

                                cmdDetalle.ExecuteNonQuery();
                            }

                            actual = actual.Siguiente;
                        }
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("\nError al guardar en la base de datos: " + ex.Message);
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }
                    return false;
                }
            }
        }
    }
}
