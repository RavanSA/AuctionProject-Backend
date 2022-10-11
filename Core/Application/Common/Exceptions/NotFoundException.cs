namespace Application.Common.Exceptions
{
    using System;

    public class NotFoundException : Exception
    {
        public NotFoundException(string name)
            : base($"Such '{name}' was not found.") { }


    }
}