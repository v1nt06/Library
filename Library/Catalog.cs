using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public static class Catalog
    {
        private static readonly List<Document> Documents = new List<Document>();

        // 1. Добавление записей в каталог.
        public static void Add(Document document)
        {
            Documents.Add(document);
        }

        // 2. Удаление записей из каталога.
        public static void Remove(int index)
        {
            Documents.Remove(Documents[index]);
        }

        // 3. Просмотр каталога.
        /* 
            Так как у нас проект типа Class library, то вывод какой-либо информации реализовать
            не возможно (например в консоль или в какой-нибудь контрол на форме). Так что этот метод 
            просто возвращает содержимое каталога.
        */
        public static IEnumerable<Document> GetCatalogContent()
        {
            return Documents;
        }

        // 4. Поиск по названию.
        public static IEnumerable<Document> FindByName(string name)
        {
            return Documents.Where(d => d.Name == name);
        }

        // 5. Сортировка по году выпуска в прямом порядке.
        // 8. Группировка записей по годам издания.
        /*
            Так как тут написано "записей", а не книг, то я буду группировать все типы (в том числе и патенты у которых нет
            такого понятия как "год издания").
        */
        public static IEnumerable<Document> GetSortedContentByPublicationDateAsc()
        {
            return Documents.OrderBy(d => d.PublicationDate);
        }

        // 5. Сортировка по году выпуска в обратном порядке.
        public static IEnumerable<Document> GetSortedContentByPublicationDateDesc()
        {
            return Documents.OrderByDescending(d => d.PublicationDate);
        }

        // 6. Поиск всех книг данного автора (в том числе, тех, у которых он является соавтором).
        public static IEnumerable<Book> GetBooksByAuthor(Person suitableAuthor)
        {
            return
                Documents.Where(
                    d =>
                        d is Book &&
                        ((Book)d).Authors.Any(
                            a => a.FirstName == suitableAuthor.FirstName && a.LastName == suitableAuthor.LastName))
                    .Cast<Book>();
        }

        // 7. Вывод всех книг, название издательства которых начинаются с заданного набора символов, с группировкой по издательству.
        public static IEnumerable<Book> GetBooksByPartOfPublisher(string partOfPublisherName)
        {
            return Documents.Where(d => d is Book && ((Book)d).Publisher.StartsWith(partOfPublisherName))
                .Cast<Book>().OrderBy(b => b.Publisher);
        }
    }
}
