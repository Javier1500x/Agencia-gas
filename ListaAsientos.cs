using System;

namespace SistemaContable
{
    public class NodoAsiento
    {
        public int Id;
        public DateTime Fecha;
        public string Descripcion;
        public ListaMovimientos Movimientos;
        public NodoAsiento Anterior;
        public NodoAsiento Siguiente;
    }
    public class ListaAsientos
    {
        public NodoAsiento Cabeza;
        public NodoAsiento Cola;
        public int Cantidad;

        public void Agregar(NodoAsiento Nuevo)
        {
            if (Cabeza == null)
            {
                Cabeza = Nuevo;
                Cola = Nuevo;
            }
            else
            {
                Cola.Siguiente = Nuevo;
                Nuevo.Anterior = Cola;
                Cola = Nuevo;
            }

            Cantidad = Cantidad + 1;
        }
    }
}