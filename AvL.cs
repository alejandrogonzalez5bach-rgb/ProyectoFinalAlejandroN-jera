using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecfinal
{
    public class DatosProducto
    {
        public String Codigoproducto { get; set; }
        public String NombrePro { get; set; }
        public String Catego { get; set; }
        public decimal Preci { get; set; }
        public int Stock { get; set; }

        public DatosProducto(string Codigoproducto, string NombrePro, string Catego, decimal Preci, int Stock)
        {
            this.Codigoproducto = Codigoproducto;
            this.NombrePro = NombrePro;
            this.Catego = Catego;
            this.Preci = Preci;
            this.Stock = Stock;
        }
    }

    public class NodAVL
    {
        public DatosProducto Producto { get; set; }
        public NodAVL Izquierdo { get; set; }
        public NodAVL Derecho { get; set; }
        public int Altura { get; set; }

        public NodAVL(DatosProducto datos)
        {
            Producto = datos;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        }
    }

    public class TreeAVL
    {
        public NodAVL Raiz;

        private int ObteAltura(NodAVL nodo)
        {
            return nodo == null ? 0 : nodo.Altura;
        }

        private int ObteFactorBalan(NodAVL nodo)
        {
            return nodo == null ? 0 : ObteAltura(nodo.Izquierdo) - ObteAltura(nodo.Derecho);
        }

        private int ObteMaximo(int a, int k)
        {
            return (a > k) ? a : k;
        }

        private NodAVL RotacionDerecha(NodAVL K)
        {
            NodAVL A = K.Izquierdo;
            NodAVL T2 = A.Derecho;

            A.Derecho = K;
            K.Izquierdo = T2;

            K.Altura = ObteMaximo(ObteAltura(K.Izquierdo), ObteAltura(K.Derecho)) + 1;
            A.Altura = ObteMaximo(ObteAltura(A.Izquierdo), ObteAltura(A.Derecho)) + 1;

            return A;
        }

        private NodAVL RotacionIzquierda(NodAVL x)
        {
            NodAVL y = x.Derecho;
            NodAVL T2 = y.Izquierdo;

            y.Izquierdo = x;
            x.Derecho = T2;

            x.Altura = ObteMaximo(ObteAltura(x.Izquierdo), ObteAltura(x.Derecho)) + 1;
            y.Altura = ObteMaximo(ObteAltura(y.Izquierdo), ObteAltura(y.Derecho)) + 1;

            return y;
        }

        public void Insertar(DatosProducto producto)
        {
            Raiz = InsertarRec(Raiz, producto);
        }

        private NodAVL InsertarRec(NodAVL nodo, DatosProducto producto)
        {
            if (nodo == null)
                return new NodAVL(producto);

            int comparacion = string.Compare(producto.Codigoproducto, nodo.Producto.Codigoproducto, StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
                nodo.Izquierdo = InsertarRec(nodo.Izquierdo, producto);
            else if (comparacion > 0)
                nodo.Derecho = InsertarRec(nodo.Derecho, producto);
            else
                return nodo;

            nodo.Altura = 1 + ObteMaximo(ObteAltura(nodo.Izquierdo), ObteAltura(nodo.Derecho));
            int balance = ObteFactorBalan(nodo);

            if (balance > 1 && string.Compare(producto.Codigoproducto, nodo.Izquierdo.Producto.Codigoproducto, StringComparison.OrdinalIgnoreCase) < 0)
                return RotacionDerecha(nodo);

            if (balance < -1 && string.Compare(producto.Codigoproducto, nodo.Derecho.Producto.Codigoproducto, StringComparison.OrdinalIgnoreCase) > 0)
                return RotacionIzquierda(nodo);

            if (balance > 1 && string.Compare(producto.Codigoproducto, nodo.Izquierdo.Producto.Codigoproducto, StringComparison.OrdinalIgnoreCase) > 0)
            {
                nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo);
                return RotacionDerecha(nodo);
            }

            if (balance < -1 && string.Compare(producto.Codigoproducto, nodo.Derecho.Producto.Codigoproducto, StringComparison.OrdinalIgnoreCase) < 0)
            {
                nodo.Derecho = RotacionDerecha(nodo.Derecho);
                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        public DatosProducto Buscar(string codigoproducto)
        {
            NodAVL resultado = BuscarRec(Raiz, codigoproducto);
            return resultado != null ? resultado.Producto : null;
        }

        private NodAVL BuscarRec(NodAVL nodo, string codigoproducto)
        {
            if (nodo == null)
                return null;

            int comparacion = string.Compare(codigoproducto, nodo.Producto.Codigoproducto, StringComparison.OrdinalIgnoreCase);

            if (comparacion == 0)
                return nodo;
            if (comparacion < 0)
                return BuscarRec(nodo.Izquierdo, codigoproducto);

            return BuscarRec(nodo.Derecho, codigoproducto);
        }

        public void Inorden(NodAVL nodo)
        {
            if (nodo != null)
            {
                Inorden(nodo.Izquierdo);
                Console.WriteLine($"Código: {nodo.Producto.Codigoproducto,-10} | Nombre: {nodo.Producto.NombrePro,-25} | Categoria: {nodo.Producto.Catego,-12} | Precio: Q{nodo.Producto.Preci,-8} | Stock: {nodo.Producto.Stock}");
                Inorden(nodo.Derecho);
            }
        }

        public bool Actualizar(string codigoproducto, string nuevoNombrepro, int nuevoStock, decimal nuevoprecio)
        {
            NodAVL nodo = BuscarRec(Raiz, codigoproducto);
            if (nodo != null)
            {
                nodo.Producto.NombrePro = nuevoNombrepro;
                nodo.Producto.Stock = nuevoStock;
                nodo.Producto.Preci = nuevoprecio;
                return true;
            }
            return false;
        }

        public void Eliminar(string codigo)
        {
            Raiz = EliminarRec(Raiz, codigo);
        }

        private NodAVL EliminarRec(NodAVL nodo, string codigo)
        {
            if (nodo == null)
                return nodo;

            int comparacion = string.Compare(codigo, nodo.Producto.Codigoproducto, StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
            {
                nodo.Izquierdo = EliminarRec(nodo.Izquierdo, codigo);
            }
            else if (comparacion > 0)
            {
                nodo.Derecho = EliminarRec(nodo.Derecho, codigo);
            }
            else
            {
                if ((nodo.Izquierdo == null) || (nodo.Derecho == null))
                {
                    NodAVL temp = nodo.Izquierdo != null ? nodo.Izquierdo : nodo.Derecho;

                    if (temp == null)
                    {
                        temp = nodo;
                        nodo = null;
                    }
                    else
                    {
                        nodo = temp;
                    }
                }
                else
                {
                    NodAVL temp = ObtenerNodoMinimo(nodo.Derecho);
                    nodo.Producto = temp.Producto;
                    nodo.Derecho = EliminarRec(nodo.Derecho, temp.Producto.Codigoproducto);
                }
            }

            if (nodo == null)
                return nodo;

            nodo.Altura = 1 + ObteMaximo(ObteAltura(nodo.Izquierdo), ObteAltura(nodo.Derecho));
            int balance = ObteFactorBalan(nodo);

            if (balance > 1 && ObteFactorBalan(nodo.Izquierdo) >= 0)
                return RotacionDerecha(nodo);

            if (balance > 1 && ObteFactorBalan(nodo.Izquierdo) < 0)
            {
                nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo);
                return RotacionDerecha(nodo);
            }

            if (balance < -1 && ObteFactorBalan(nodo.Derecho) <= 0)
                return RotacionIzquierda(nodo);

            if (balance < -1 && ObteFactorBalan(nodo.Derecho) > 0)
            {
                nodo.Derecho = RotacionDerecha(nodo.Derecho);
                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        private NodAVL ObtenerNodoMinimo(NodAVL nodo)
        {
            NodAVL actual = nodo;
            while (actual.Izquierdo != null)
            {
                actual = actual.Izquierdo;
            }
            return actual;
        }

        public int ContarProductos(NodAVL nodo)
        {
            if (nodo == null) return 0;
            return 1 + ContarProductos(nodo.Izquierdo) + ContarProductos(nodo.Derecho);
        }

        public int ObtenerStockTotal(NodAVL nodo)
        {
            if (nodo == null) return 0;
            return nodo.Producto.Stock + ObtenerStockTotal(nodo.Izquierdo) + ObtenerStockTotal(nodo.Derecho);
        }
    }
}