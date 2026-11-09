namespace ListasLigadas;

public class ListException : Exception
{
    public ListException(string mensaje) : base(mensaje) { }
}

public class EmptyListException : ListException
{
    public EmptyListException() : base("La lista está vacía.") { }
}

public class ElementNotFoundException : ListException
{
    public ElementNotFoundException(object valor)
        : base($"No existe el {valor} en la lista.") { }
}
