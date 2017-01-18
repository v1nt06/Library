using System;

namespace Library
{
    public sealed class LoadCatalogException : Exception
    {
        public LoadCatalogException(string message, Exception innerException) : base(message, innerException) { }
    }
}
