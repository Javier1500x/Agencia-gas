using System;

namespace SistemaContable
{
    public class Nodo
    {
        public Movimiento Dato;
        public Nodo Siguiente;
    }
    public class ListaMovimientos
    {
        public Nodo Cabeza;
        public int Cantidad;

        public void Agregar(Movimiento Nuevo)
        {
            Nodo nuevoNodo = new Nodo();
            nuevoNodo.Dato = Nuevo;

            if (Cabeza == null)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                Nodo actual = Cabeza;

            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

                actual.Siguiente = nuevoNodo;
            }

                Cantidad = Cantidad + 1;
        }
    }
}