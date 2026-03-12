namespace KucniSavetBackend.Exceptions;


public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }
}

public class NotFoundException<T> : NotFoundException
{
    public NotFoundException(string key) : base($"{typeof(T).Name} {key} not found.")
    { 
        
    }
}

