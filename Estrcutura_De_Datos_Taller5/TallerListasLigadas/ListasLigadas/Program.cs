using ListasLigadas;

NodoArbol raiz = new NodoArbol("Raíz");

NodoArbol enteros = new NodoArbol("Enteros (int)");
enteros.Agregar(CrearHoja<int>("Lista de enteros A", t => int.Parse(t)));
enteros.Agregar(CrearHoja<int>("Lista de enteros B", t => int.Parse(t)));

NodoArbol textos = new NodoArbol("Textos (string)");
textos.Agregar(CrearHoja<string>("Lista de textos A", t => t));
textos.Agregar(CrearHoja<string>("Lista de textos B", t => t));

NodoArbol decimales = new NodoArbol("Decimales (double)");
decimales.Agregar(CrearHoja<double>("Lista de decimales A", t => double.Parse(t)));
decimales.Agregar(CrearHoja<double>("Lista de decimales B", t => double.Parse(t)));

raiz.Agregar(enteros);
raiz.Agregar(textos);
raiz.Agregar(decimales);

Console.WriteLine("=== TALLER 5 - ÁRBOL DE LISTAS DOBLEMENTE LIGADAS ===");
Navegar(raiz, raiz.Nombre);

static NodoArbol CrearHoja<T>(string nombre, Func<string, T> convertir) where T : IComparable<T>
{
    ListaDoble<T> lista = new ListaDoble<T>();
    return new NodoArbol(nombre, () => Menu(lista, convertir));
}

static void Navegar(NodoArbol nodo, string ruta)
{
    if (nodo.Accion != null)
    {
        Console.WriteLine();
        Console.WriteLine($">> Trabajando en: {ruta}");
        nodo.Accion();
        return;
    }

    string opcion;
    do
    {
        Console.WriteLine();
        Console.WriteLine($"---------- {ruta} ----------");
        for (int i = 0; i < nodo.Hijos.Count; i++)
            Console.WriteLine($"{i + 1}. {nodo.Hijos[i].Nombre}");
        Console.WriteLine("T. Ver árbol completo");
        Console.WriteLine("0. " + (nodo.Padre == null ? "Salir" : "Volver"));
        Console.Write("Opción: ");
        opcion = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (opcion == "T")
        {
            Console.WriteLine();
            ImprimirArbol(BuscarRaiz(nodo), "", true);
        }
        else if (int.TryParse(opcion, out int indice) && indice >= 1 && indice <= nodo.Hijos.Count)
        {
            NodoArbol hijo = nodo.Hijos[indice - 1];
            Navegar(hijo, $"{ruta} > {hijo.Nombre}");
        }
        else if (opcion != "0")
        {
            Console.WriteLine("Opción no válida.");
        }
    } while (opcion != "0");
}

static NodoArbol BuscarRaiz(NodoArbol nodo)
{
    while (nodo.Padre != null) nodo = nodo.Padre;
    return nodo;
}

static void ImprimirArbol(NodoArbol nodo, string prefijo, bool esUltimo)
{
    if (nodo.Padre == null)
    {
        Console.WriteLine(nodo.Nombre);
    }
    else
    {
        Console.WriteLine(prefijo + (esUltimo ? "└── " : "├── ") + nodo.Nombre);
        prefijo += esUltimo ? "    " : "│   ";
    }

    for (int i = 0; i < nodo.Hijos.Count; i++)
        ImprimirArbol(nodo.Hijos[i], prefijo, i == nodo.Hijos.Count - 1);
}

static void Menu<T>(ListaDoble<T> lista, Func<string, T> convertir) where T : IComparable<T>
{
    string opcion;

    do
    {
        MostrarMenu();
        opcion = Console.ReadLine() ?? "";
        Console.WriteLine();

        switch (opcion)
        {
            case "1":
                if (LeerDato(convertir, out T? nuevo))
                {
                    lista.Adicionar(nuevo!);
                    Console.WriteLine("Elemento agregado.");
                }
                break;

            case "2":
                Console.WriteLine(lista.MostrarAdelante());
                break;

            case "3":
                Console.WriteLine(lista.MostrarAtras());
                break;

            case "4":
                lista.OrdenarDescendentemente();
                Console.WriteLine("Lista ordenada descendentemente.");
                break;

            case "5":
                Console.WriteLine(lista.MostrarModas());
                break;

            case "6":
                Console.WriteLine(lista.MostrarGrafico());
                break;

            case "7":
                if (LeerDato(convertir, out T? buscado))
                {
                    if (lista.Existe(buscado!))
                        Console.WriteLine($"Sí existe el {buscado}.");
                    else
                        Console.WriteLine($"No existe el {buscado}.");
                }
                break;

            case "8":
                if (LeerDato(convertir, out T? unico))
                {
                    if (lista.EliminarUna(unico!))
                        Console.WriteLine($"Se eliminó una ocurrencia del {unico}.");
                    else
                        Console.WriteLine($"No existe el {unico}.");
                }
                break;

            case "9":
                if (LeerDato(convertir, out T? todos))
                {
                    int cuantos = lista.EliminarTodas(todos!);
                    if (cuantos > 0)
                        Console.WriteLine($"Se eliminaron {cuantos} ocurrencia(s) del {todos}.");
                    else
                        Console.WriteLine($"No existe el {todos}.");
                }
                break;

            case "0":
                Console.WriteLine("Volviendo al árbol...");
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }

        Console.WriteLine();
    } while (opcion != "0");
}

static bool LeerDato<T>(Func<string, T> convertir, out T? dato)
{
    Console.Write("Ingrese el valor: ");
    string texto = Console.ReadLine() ?? "";

    try
    {
        dato = convertir(texto);
        return true;
    }
    catch (Exception ex) when (ex is FormatException || ex is OverflowException)
    {
        Console.WriteLine("Valor inválido para el tipo seleccionado.");
        dato = default;
        return false;
    }
}

static void MostrarMenu()
{
    Console.WriteLine("---------- MENÚ DE LA LISTA ----------");
    Console.WriteLine("1. Adicionar");
    Console.WriteLine("2. Mostrar hacia adelante");
    Console.WriteLine("3. Mostrar hacia atrás");
    Console.WriteLine("4. Ordenar descendentemente");
    Console.WriteLine("5. Mostrar la(s) moda(s)");
    Console.WriteLine("6. Mostrar gráfico");
    Console.WriteLine("7. Existe");
    Console.WriteLine("8. Eliminar una ocurrencia");
    Console.WriteLine("9. Eliminar todas las ocurrencias");
    Console.WriteLine("0. Volver");
    Console.Write("Opción: ");
}

class NodoArbol
{
    public string Nombre { get; }
    public NodoArbol? Padre { get; private set; }
    public List<NodoArbol> Hijos { get; } = new List<NodoArbol>();
    public Action? Accion { get; }

    public NodoArbol(string nombre, Action? accion = null)
    {
        Nombre = nombre;
        Accion = accion;
    }

    public void Agregar(NodoArbol hijo)
    {
        hijo.Padre = this;
        Hijos.Add(hijo);
    }
}