using System.Text;

namespace ListasLigadas;

public class ListaDoble<T> where T : IComparable<T>
{
    private Nodo<T>? cabeza;
    private Nodo<T>? cola;
    private int cantidad;
    private bool descendente = false;

    public int Cantidad => cantidad;
    public bool EstaVacia => cabeza == null;

    private int Comparar(T a, T b)
    {
        return descendente ? b.CompareTo(a) : a.CompareTo(b);
    }

    public void Adicionar(T dato)
    {
        Nodo<T> nuevo = new Nodo<T>(dato);

        if (cabeza == null)
        {
            cabeza = nuevo;
            cola = nuevo;
            cantidad++;
            return;
        }

        Nodo<T>? actual = cabeza;
        while (actual != null && Comparar(dato, actual.Dato) >= 0)
        {
            actual = actual.Siguiente;
        }

        if (actual == null)
        {
            cola!.Siguiente = nuevo;
            nuevo.Anterior = cola;
            cola = nuevo;
        }
        else
        {
            nuevo.Siguiente = actual;
            nuevo.Anterior = actual.Anterior;

            if (actual.Anterior != null)
                actual.Anterior.Siguiente = nuevo;
            else
                cabeza = nuevo;

            actual.Anterior = nuevo;
        }

        cantidad++;
    }

    public string MostrarAdelante()
    {
        if (cabeza == null) return "La lista está vacía.";

        StringBuilder sb = new StringBuilder();
        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            sb.Append(actual.Dato);
            if (actual.Siguiente != null) sb.Append(", ");
            actual = actual.Siguiente;
        }
        return sb.ToString();
    }

    public string MostrarAtras()
    {
        if (cola == null) return "La lista está vacía.";

        StringBuilder sb = new StringBuilder();
        Nodo<T>? actual = cola;
        while (actual != null)
        {
            sb.Append(actual.Dato);
            if (actual.Anterior != null) sb.Append(", ");
            actual = actual.Anterior;
        }
        return sb.ToString();
    }

    public void OrdenarDescendentemente()
    {
        if (descendente || cantidad < 2)
        {
            descendente = true;
            return;
        }

        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            Nodo<T>? temp = actual.Siguiente;
            actual.Siguiente = actual.Anterior;
            actual.Anterior = temp;

            actual = actual.Anterior;
        }

        Nodo<T>? aux = cabeza;
        cabeza = cola;
        cola = aux;

        descendente = true;
    }

    private List<(T Valor, int Veces)> ObtenerOcurrencias()
    {
        List<(T Valor, int Veces)> resultado = new List<(T, int)>();

        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            if (resultado.Count > 0 &&
                resultado[resultado.Count - 1].Valor.CompareTo(actual.Dato) == 0)
            {
                var ultimo = resultado[resultado.Count - 1];
                resultado[resultado.Count - 1] = (ultimo.Valor, ultimo.Veces + 1);
            }
            else
            {
                resultado.Add((actual.Dato, 1));
            }
            actual = actual.Siguiente;
        }

        return resultado;
    }

    public string MostrarModas()
    {
        if (cabeza == null) return "La lista está vacía.";

        var ocurrencias = ObtenerOcurrencias();

        int max = 0;
        foreach (var o in ocurrencias)
            if (o.Veces > max) max = o.Veces;

        List<T> modas = new List<T>();
        foreach (var o in ocurrencias)
            if (o.Veces == max) modas.Add(o.Valor);

        if (modas.Count == 1)
            return $"La moda es {modas[0]}.";

        StringBuilder sb = new StringBuilder("Las modas son ");
        for (int i = 0; i < modas.Count; i++)
        {
            sb.Append(modas[i]);
            if (i < modas.Count - 2) sb.Append(", ");
            else if (i == modas.Count - 2) sb.Append(" y ");
        }
        sb.Append('.');
        return sb.ToString();
    }

    public string MostrarGrafico()
    {
        if (cabeza == null) return "La lista está vacía.";

        StringBuilder sb = new StringBuilder();
        foreach (var o in ObtenerOcurrencias())
        {
            sb.AppendLine($"{o.Valor} {new string('*', o.Veces)}");
        }
        return sb.ToString();
    }

    public bool Existe(T dato)
    {
        return BuscarNodo(dato) != null;
    }

    private Nodo<T>? BuscarNodo(T dato)
    {
        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            if (actual.Dato.CompareTo(dato) == 0)
                return actual;
            actual = actual.Siguiente;
        }
        return null;
    }

    private void DesenlazarNodo(Nodo<T> nodo)
    {
        if (nodo.Anterior != null)
            nodo.Anterior.Siguiente = nodo.Siguiente;
        else
            cabeza = nodo.Siguiente;

        if (nodo.Siguiente != null)
            nodo.Siguiente.Anterior = nodo.Anterior;
        else
            cola = nodo.Anterior;

        cantidad--;
    }

    public bool EliminarUna(T dato)
    {
        Nodo<T>? nodo = BuscarNodo(dato);
        if (nodo == null) return false;

        DesenlazarNodo(nodo);
        return true;
    }

    public int EliminarTodas(T dato)
    {
        int eliminados = 0;
        Nodo<T>? actual = cabeza;

        while (actual != null)
        {
            Nodo<T>? siguiente = actual.Siguiente;

            if (actual.Dato.CompareTo(dato) == 0)
            {
                DesenlazarNodo(actual);
                eliminados++;
            }
            actual = siguiente;
        }

        return eliminados;
    }
}