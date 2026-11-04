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