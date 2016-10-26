using System;

namespace Library
{
    public interface IDocument : IComparable
    {
        string Name { get; }
        int PagesCount { get; }
        string Annotation { get; set; }
        DateTime PublicationDate { get; }
    }
}
