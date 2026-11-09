# Taller5_ListasLigadas_EstructuraDedatos

Taller 5 – Lista doblemente ligada genérica (C# / .NET 8)

Lista doblemente ligada genérica (ListaDoble<T>) que mantiene sus elementos en orden ascendente al insertarlos, con un programa de consola para operar sobre ella. Funciona con cualquier tipo que implemente IComparable<T> (int, string, double, etc.).

Cómo ejecutar
bash
cd Estrcutura_De_Datos_Taller5/TallerListasLigadas/ListasLigadas
dotnet run

El menú inicial es un árbol para escoger el tipo de lista (enteros, textos, decimales). Dentro de cada lista están las opciones del taller: adicionar, mostrar adelante/atrás, ordenar descendentemente, moda(s), gráfico, existe, eliminar una ocurrencia y eliminar todas.

Estructura
Archivo	Qué hace
Nodo.cs	Nodo con Dato, Siguiente y Anterior
ListaDoble.cs	La lista y todas sus operaciones
ListException.cs	Excepciones propias: EmptyListException (operar sobre lista vacía) y ElementNotFoundException (eliminar un valor que no está)
Program.cs	Menú de consola y árbol de navegación
¿Para qué sirve una lista doblemente ligada?

Cada nodo conoce a su anterior y a su siguiente, por eso se puede recorrer en ambos sentidos y desenlazar un nodo en O(1) si ya se tiene su referencia (no hay que desplazar elementos como en un arreglo). Su costo: más memoria por nodo (dos punteros) y no hay acceso directo por índice.

Cómo se ve en la industria
Las librerías estándar la traen hecha: LinkedList<T> en .NET, LinkedList en Java y std::list en C++ son listas doblemente ligadas.
Cachés LRU: tabla hash + lista doble. Al usar un elemento se mueve al frente en O(1) y se expulsa el de la cola.
Historial del navegador, deshacer/rehacer, reproductores de música: moverse hacia adelante y hacia atrás.
Núcleo de Linux y planificadores de procesos: usan listas doblemente ligadas porque insertar y sacar elementos es barato.
Aun así, en la práctica List<T> (arreglo dinámico) suele ser más rápido en recorridos por la localidad de caché; la lista ligada gana cuando hay muchas inserciones/eliminaciones sobre nodos ya localizados.
Para mantener datos ordenados con muchas inserciones se prefieren estructuras como SortedSet<T> (árbol balanceado, O(log n)) o un heap. Una lista ordenada es didáctica, pero insertar en orden cuesta O(n).
Relación con análisis de algoritmos

Con n elementos y k valores distintos, estas son las complejidades de esta implementación:

Operación	Tiempo	Por qué
Adicionar (en orden)	O(n)	Recorre desde la cabeza hasta hallar la posición
Mostrar adelante / atrás	O(n)	Recorre toda la lista
Ordenar descendentemente	O(n)	Solo invierte punteros; como ya está ordenada no se necesita un O(n log n)
Moda(s)	O(n) tiempo, O(k) espacio	Una pasada: al estar ordenada, los iguales quedan juntos y no hace falta un diccionario
Gráfico	O(n)	Reutiliza el conteo de ocurrencias
Existe	O(n)	Búsqueda lineal (no hay búsqueda binaria sin índices)
Eliminar una ocurrencia	O(n)	Buscar es O(n); desenlazar es O(1)
Eliminar todas	O(n)	Una sola pasada guardando el Siguiente antes de desenlazar
Espacio total	O(n)	Un nodo con dos referencias por elemento

Comparación con un arreglo/List<T>: acceso por índice O(1) vs O(n); insertar o eliminar en el medio O(n) (por desplazar) vs O(1) una vez ubicado el nodo. La lección del taller es la misma que busca el análisis de algoritmos: elegir la estructura según qué operaciones se hacen más y entender su costo, no solo que el programa funcione.

Posible mejora: comparar primero contra la cola, así insertar un valor mayor o igual al último sería O(1) en vez de O(n).
