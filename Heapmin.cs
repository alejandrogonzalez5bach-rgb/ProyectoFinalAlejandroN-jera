using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecfinal
{
    public class EstruCliente
    {
        public string Nombre { get; set; }
        public int TipoDcliente { get; set; }
        public int Numerodproducto { get; set; }
        public int Hradellegada { get; set; }

        public EstruCliente(string Nombre, int TipoDcliente, int Numerodproducto, int Hradellegada)
        {
            this.Nombre = Nombre;
            this.TipoDcliente = TipoDcliente;
            this.Numerodproducto = Numerodproducto;
            this.Hradellegada = Hradellegada;
        }

        public string ObtePrioriClientes()
        {
            return TipoDcliente switch
            {
                1 => "VIP",
                2 => "ADULTO_MAYOR",
                3 => "REGULAR",
                _ => "Desconocido"
            };
        }
    }

    public class MinHeap
    {
        private List<EstruCliente> heap = new List<EstruCliente>();

        public int Cantidad => heap.Count;

        public bool ExisteCodigo(string codigo)
        {
            foreach (var k in heap)
            {
                if (k.Nombre.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public void Insertar(EstruCliente ticket)
        {
            heap.Add(ticket);
            BubbleUp(heap.Count - 1);
        }

        public EstruCliente Peek()
        {
            if (heap.Count == 0) return null;
            return heap[0];
        }

        public EstruCliente ExtractMin()
        {
            if (heap.Count == 0) return null;

            EstruCliente minTicket = heap[0];
            EstruCliente ultimoTicket = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                heap[0] = ultimoTicket;
                BubbleDown(0);
            }

            return minTicket;
        }

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (heap[index].TipoDcliente < heap[parentIndex].TipoDcliente)
                {
                    Intercambiar(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void BubbleDown(int index)
        {
            while (index < heap.Count)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int smallest = index;

                if (leftChild < heap.Count && heap[leftChild].TipoDcliente < heap[smallest].TipoDcliente)
                {
                    smallest = leftChild;
                }

                if (rightChild < heap.Count && heap[rightChild].TipoDcliente < heap[smallest].TipoDcliente)
                {
                    smallest = rightChild;
                }

                if (smallest != index)
                {
                    Intercambiar(index, smallest);
                    index = smallest;
                }
                else
                {
                    break;
                }
            }
        }

        private void Intercambiar(int i, int j)
        {
            EstruCliente temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        public List<EstruCliente> ListaHeap()
        {
            return new List<EstruCliente>(heap);
        }

        public EstruCliente BuscarCodigo(string codigo)
        {
            foreach (var ticket in heap)
            {
                if (ticket.Nombre.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                {
                    return ticket;
                }
            }
            return null;
        }

        public int ContarPorTipo(int tipo)
        {
            int cuenta = 0;
            foreach (var cliente in ListaHeap())
            {
                if (cliente.TipoDcliente == tipo)
                    cuenta++;
            }
            return cuenta;
        }
    }
}