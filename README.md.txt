========================================================================
 PROYECTO FINAL: SISTEMA DE GESTIÓN DE SUPERMERCADO
 CURSO: Procesos y Algoritmos II
 ESTUDIANTE: Alejandro Gustavo González Nájera (Carnet: 2627024)
 INSTITUCIÓN: Universidad Regional de Guatemala
 FACULTAD: Administración en Sistemas Informáticos
========================================================================

DESCRIPCIÓN DEL SISTEMA:
Aplicación de consola en C# (.NET) desarrollada para optimizar la gestión
de inventario de productos y el control de atención prioritaria de clientes.

ESTRUCTURAS DE DATOS IMPLEMENTADAS:
1. Árbol AVL (KatArbol - Inventario de Productos):
   - Almacena objetos de la clase 'DatosProducto' ordenados por código.
   - Mantiene balanceo automático tras cada inserción y eliminación mediante rotaciones.
   - Ofrece operaciones de inserción, búsqueda, actualización, eliminación y recorrido Inorden.

2. Min Heap (colaPrioridad - Atención de Clientes):
   - Organiza la fila de clientes asignando prioridad según su tipo y orden de llegada (FIFO).
   - Implementa algoritmos de reordenamiento eficiente en O(log n).

ESTRUCTURA DEL CÓDIGO FUENTE:
- Program.cs: Menú principal unificado de 10 opciones y control de consola.
- Clases de Inventario (DatosProducto, NodAVL, TreeAVL): Manejo del Árbol AVL.
- Clases de Clientes (EstruCliente, MinHeap): Manejo de la Cola de Prioridad.

INSTRUCCIONES DE EJECUCIÓN:
1. Abrir la carpeta del proyecto en Microsoft Visual Studio.
2. Asegurarse de abrir el archivo Program.cs.
3. Presionar el botón "Iniciar" (Play / F5) para correr la consola interactiva.
========================================================================