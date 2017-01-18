using System;

namespace Library
{
    public sealed class SaveCatalogException : Exception
    {
        public SaveCatalogException(string message, Exception innerException) : base(message, innerException) { }
    }
}
