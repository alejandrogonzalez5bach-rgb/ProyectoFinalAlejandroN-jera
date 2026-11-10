using System;
using System.Collections.Generic;

namespace Proyecfinal
{
    internal class Program
    {
        static MinHeap colaPrioridad = new MinHeap();
        static void Main(string[] args)
        {
            TreeAVL KatArbol = new TreeAVL();
            bool Moshi = true;

            while (Moshi == true)
            {
                Console.Clear();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("   SISTEMA DE GESTIÓN DE SUPERMERCADO   ");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("-------- INVENTARIO (AVL) ----------");
                Console.WriteLine();
                Console.WriteLine("1. Registrar producto");
                Console.WriteLine("2. Buscar producto por código");
                Console.WriteLine("3. Actualizar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Mostrar inventario ordenado");
                Console.WriteLine();
                Console.WriteLine("------ ATENCIÓN DE CLIENTES (HEAP) -----");
                Console.WriteLine();
                Console.WriteLine("6. Agregar cliente a la cola");
                Console.WriteLine("7. Atender siguiente cliente");
                Console.WriteLine("8. Mostrar clientes en espera");
                Console.WriteLine();
                Console.WriteLine("------------ REPORTES ------------");
                Console.WriteLine();
                Console.WriteLine("9. Mostrar estadísticas del sistema");
                Console.WriteLine();
                Console.WriteLine("------------ SISTEMA ------------");
                Console.WriteLine();
                Console.WriteLine("10. Salir");
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out int opcionMenu))
                {
                    switch (opcionMenu)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("===== REGISTRO PRODUCTO =====");
                            Console.Write("Ingrese el Código (ej. PRO0001): ");
                            String codigoInput = Console.ReadLine().Trim();

                            String codigoLimpio = codigoInput.Replace("PRO", "").Replace("pro", "").Trim();
                            String codigoproducto = "PRO" + codigoLimpio.PadLeft(4, '0');

                            if (KatArbol.Buscar(codigoproducto) != null)
                            {
                                Console.WriteLine($"\nError: Ya existe un producto con el código {codigoproducto}.");
                                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                                Console.ReadKey();
                                break;
                            }

                            Console.Write("Nombre del producto: ");
                            String NombrePro = Console.ReadLine().Trim();

                            Console.Write("Precio del producto: ");
                            decimal preci;
                            while (!decimal.TryParse(Console.ReadLine(), out preci) || preci <= 0)
                            {
                                Console.WriteLine("\nError: Ingrese un precio válido mayor a 0.");
                                Console.Write("Introduzca de nuevo el precio: ");
                            }

                            Console.Write("Número de Stock: ");
                            int Stock;
                            while (!int.TryParse(Console.ReadLine(), out Stock) || Stock < 0)
                            {
                                Console.WriteLine("\nError: Ingrese un stock válido (mayor o igual a 0).");
                                Console.Write("Introduzca de nuevo el Stock: ");
                            }

                            Console.WriteLine("Tipos de Categoria (\"Alimentos\", \"Bebidas\", \"Electrónica\")");
                            Console.Write("Ingrese tipo de Categoria: ");
                            String Catego = Console.ReadLine().Trim();

                            DatosProducto nuevoPaciente = new DatosProducto(codigoproducto, NombrePro, Catego, preci, Stock);
                            KatArbol.Insertar(nuevoPaciente);

                            Console.WriteLine($"\nProducto {codigoproducto} insertado correctamente.");
                            Console.WriteLine("\nPresione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("\n=== BUSCAR PRODUCTO ===");
                            Console.Write("Ingrese el código del producto (ej. PRO0001): ");
                            String busquedaInput = Console.ReadLine();

                            if (!String.IsNullOrEmpty(busquedaInput))
                            {
                                string cLimpio = busquedaInput.Replace("PRO", "").Replace("pro", "").Trim();
                                String codigoBuscado = "PRO" + cLimpio.PadLeft(4, '0');

                                DatosProducto productoEncontrado = KatArbol.Buscar(codigoBuscado);

                                if (productoEncontrado != null)
                                {
                                    Console.WriteLine("\nProducto encontrado:");
                                    Console.WriteLine($"\nCódigo     : {productoEncontrado.Codigoproducto}");
                                    Console.WriteLine($"Nombre     : {productoEncontrado.NombrePro}");
                                    Console.WriteLine($"Categoría  : {productoEncontrado.Catego}");
                                    Console.WriteLine($"Precio     : Q{productoEncontrado.Preci}");
                                    Console.WriteLine($"Stock      : {productoEncontrado.Stock}");
                                }
                                else
                                {
                                    Console.WriteLine("\nProducto no encontrado.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNo ingresó un código válido.");
                            }

                            Console.WriteLine("\nPresione cualquier tecla para regresar al menú...");
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("\n=== ACTUALIZAR DATOS DE PRODUCTOS ===");
                            Console.Write("Ingrese el código del producto a actualizar (ej. PRO0001): ");
                            String codAct = Console.ReadLine().Trim();

                            String cActLimpio = codAct.Replace("PRO", "").Replace("pro", "").Trim();
                            String codigoActualizar = "PRO" + cActLimpio.PadLeft(4, '0');

                            var prodExistente = KatArbol.Buscar(codigoActualizar);
                            if (prodExistente != null)
                            {
                                Console.Write("Nuevo nombre del producto: ");
                                String nuevoNom = Console.ReadLine();

                                Console.Write("Nueva categoría: ");
                                String nuevaCat = Console.ReadLine();

                                Console.Write("Nuevo stock: ");
                                int nuevoStk = int.Parse(Console.ReadLine());

                                Console.Write("Nuevo precio: ");
                                decimal nuevoPre = decimal.Parse(Console.ReadLine());

                                KatArbol.Actualizar(codigoActualizar, nuevoNom, nuevoStk, nuevoPre);
                                Console.WriteLine("\nProducto actualizado correctamente.");
                                Console.WriteLine($"\nCódigo     : {codigoActualizar}");
                                Console.WriteLine($"Nombre     : {nuevoNom}");
                                Console.WriteLine($"Categoría  : {nuevaCat}");
                                Console.WriteLine($"Precio     : Q{nuevoPre}");
                                Console.WriteLine($"Stock      : {nuevoStk}");
                            }
                            else
                            {
                                Console.WriteLine($"\nError: No existe un producto con el código {codigoActualizar}.");
                            }

                            Console.WriteLine("\nPresione cualquier tecla para regresar...");
                            Console.ReadKey();
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("\n=== ELIMINAR PRODUCTO ===");
                            Console.Write("Ingrese el código del producto a eliminar (ej. PRO0001): ");
                            String codDel = Console.ReadLine().Trim();

                            String cDelLimpio = codDel.Replace("PRO", "").Replace("pro", "").Trim();
                            String codigoEliminar = "PRO" + cDelLimpio.PadLeft(4, '0');

                            var prodDel = KatArbol.Buscar(codigoEliminar);
                            if (prodDel != null)
                            {
                                KatArbol.Eliminar(codigoEliminar);
                                Console.WriteLine($"\nProducto {codigoEliminar} eliminado correctamente.");
                            }
                            else
                            {
                                Console.WriteLine($"\nError: No existe un producto con el código {codigoEliminar}.");
                            }

                            Console.WriteLine("\nPresione cualquier tecla para regresar...");
                            Console.ReadKey();
                            break;

                        case 5:
                            Console.Clear();
                            Console.WriteLine("======================================================================");
                            Console.WriteLine("                       INVENTARIO DEL SUPERMERCADO                    ");
                            Console.WriteLine("======================================================================");
                            Console.WriteLine();
                            if (KatArbol.Raiz == null)
                                Console.WriteLine("El inventario está vacío.");
                            else
                                KatArbol.Inorden(KatArbol.Raiz);

                            Console.WriteLine("\nPresione cualquier tecla para regresar...");
                            Console.ReadKey();
                            break;

                        case 6:
                            AgregarCliente();
                            break;

                        case 7:
                            AtenderCliente();
                            break;

                        case 8:
                            MostrarClienteenEspera();
                            break;

                        case 9:
                            Console.Clear();
                            Console.WriteLine("=======================================");
                            Console.WriteLine("     === ESTADÍSTICAS DEL SISTEMA ===");
                            Console.WriteLine("=======================================");

                            int totalProductos = KatArbol.ContarProductos(KatArbol.Raiz);
                            int stockTotal = KatArbol.ObtenerStockTotal(KatArbol.Raiz);

                            Console.WriteLine("\n--- INVENTARIO (ÁRBOL AVL) ---");
                            Console.WriteLine($"Total de productos registrados: {totalProductos}");
                            Console.WriteLine($"Stock total en inventario    : {stockTotal}");

                            int totalClientes = colaPrioridad.Cantidad;
                            int vip = colaPrioridad.ContarPorTipo(1);
                            int adultoMayor = colaPrioridad.ContarPorTipo(2);
                            int regular = colaPrioridad.ContarPorTipo(3);

                            Console.WriteLine("\n--- ATENCIÓN DE CLIENTES (MINHEAP) ---");
                            Console.WriteLine($"Clientes en espera (Total)   : {totalClientes}");
                            Console.WriteLine($"  - VIP                      : {vip}");
                            Console.WriteLine($"  - Adulto Mayor             : {adultoMayor}");
                            Console.WriteLine($"  - Regular                  : {regular}");

                            Console.WriteLine("=======================================");
                            Console.WriteLine("\nPresione cualquier tecla para regresar...");
                            Console.ReadKey();
                            break;

                        case 10:
                            Console.Clear();
                            Console.WriteLine("\n=======================================");
                            Console.WriteLine("   TIENDA DON JUAN");
                            Console.WriteLine("=======================================");
                            Console.WriteLine("  Gracias por utilizar el sistema.");
                            Console.WriteLine("  Cerrando aplicacion...");
                            Console.WriteLine("=======================================");

                            Moshi = false;

                            Console.WriteLine("\nPresione cualquier tecla para finalizar.");
                            Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine("\nOpción inválida. Presione ENTER para continuar.");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }

        static void AgregarCliente()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                 REGISTRO DE CLIENTE                ");
            Console.WriteLine("====================================================");
            Console.WriteLine();

            Console.Write("Nombre del Cliente          : ");
            string Nombre = Console.ReadLine().Trim();

            int TipoDcliente = 0;
            while (true)
            {
                Console.Write("Tipo de cliente (1: VIP, 2: Adulto Mayor, 3: Regular): ");
                if (int.TryParse(Console.ReadLine(), out TipoDcliente) && TipoDcliente >= 1 && TipoDcliente <= 3)
                {
                    break;
                }
                Console.WriteLine("Tipo inválido. Debe ser un número entre 1 y 3.");
            }

            Console.Write("Número de productos a comprar: ");
            int numProd = int.Parse(Console.ReadLine());

            int horaLlegada = Environment.TickCount;

            EstruCliente nuevo = new EstruCliente(Nombre, TipoDcliente, numProd, horaLlegada);
            colaPrioridad.Insertar(nuevo);

            Console.WriteLine("\n----------------------------------------------------");
            Console.WriteLine($"Cliente {Nombre} agregado correctamente.");
            Console.WriteLine($"Posición estimada en la cola: {colaPrioridad.Cantidad}");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("\nPresione ENTER para regresar al menú...");
            Console.ReadLine();
        }

        static void MostrarClienteenEspera()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("               === CLIENTES EN ESPERA ===           ");
            Console.WriteLine("====================================================");
            Console.WriteLine();

            var lista = colaPrioridad.ListaHeap();

            if (lista.Count == 0)
            {
                Console.WriteLine("La cola de prioridad se encuentra vacía.");
            }
            else
            {
                Console.WriteLine("Posición | Nombre                   | Tipo         | Productos");
                Console.WriteLine("---------------------------------------------------------------");
                for (int i = 0; i < lista.Count; i++)
                {
                    Console.WriteLine($"{i + 1,-9} | {lista[i].Nombre,-24} | {lista[i].ObtePrioriClientes(),-12} | {lista[i].Numerodproducto}");
                }
            }

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("\nPresione ENTER para regresar al menú...");
            Console.ReadLine();
        }

        static void AtenderCliente()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                    ATENDER CLIENTE                 ");
            Console.WriteLine("====================================================");
            Console.WriteLine();

            EstruCliente atendido = colaPrioridad.ExtractMin();

            if (atendido == null)
            {
                Console.WriteLine("La cola de prioridad se encuentra vacía.");
            }
            else
            {
                Console.WriteLine("Atendiendo cliente:\n");
                Console.WriteLine($"Nombre     : {atendido.Nombre}");
                Console.WriteLine($"Tipo       : {atendido.ObtePrioriClientes()}");
                Console.WriteLine($"Productos  : {atendido.Numerodproducto}");
                Console.WriteLine("\nCliente atendido correctamente.");
            }

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("\nPresione ENTER para regresar al menú...");
            Console.ReadLine();
        }
    }
}