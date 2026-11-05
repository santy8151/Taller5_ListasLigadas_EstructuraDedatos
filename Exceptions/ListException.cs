using System;

namespace Workshop5.Exceptions
{
    public class ListException : Exception
    {
        public ListException(string message) : base(message) { }
    }

    public class EmptyListException : ListException
    {
        public EmptyListException() : base("The list is empty.") { }
        public EmptyListException(string message) : base(message) { }
    }

    public class ElementNotFoundException : ListException
    {
        public ElementNotFoundException(object value) 
            : base($"Element '{value}' does not exist in the list.") { }
    }
}
