using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public static void Save(string actualFile)
        {
            File.Delete(actualFile);
            using (var sw = File.AppendText(actualFile))
            {
                foreach (var document in Documents)
                {
                    if (document is Book)
                    {
                        var sb = new StringBuilder();
                        var book = (Book) document;
                        sb.Append("Book;")
                            .Append(book.ISBN).Append(";")
                            .Append(book.Annotation ?? "null").Append(";")
                            .Append(book.Name).Append(";")
                            .Append(book.PagesCount).Append(";")
                            .Append(book.PlaceOfPublication).Append(";")
                            .Append(book.PublicationDate.ToString("yyyy-MM-dd")).Append(";")
                            .Append(book.Publisher).Append(";");

                        foreach (var author in book.Authors)
                        {
                            sb.Append(author.FirstName).Append(",");
                            sb.Append(author.LastName).Append(";");
                        }

                        sb.Append(sb.ToString().GetHashCode());

                        sw.WriteLine(sb);

                    }
                    else if (document is Newspaper)
                    {
                        var sb = new StringBuilder();
                        var newspaper = (Newspaper) document;
                        sb.Append("Newspaper").Append(";")
                            .Append(newspaper.ISSN).Append(";")
                            .Append(newspaper.Number).Append(";")
                            .Append(newspaper.Name).Append(";")
                            .Append(newspaper.Annotation ?? "null").Append(";")
                            .Append(newspaper.PagesCount).Append(";")
                            .Append(newspaper.PlaceOfPublication).Append(";")
                            .Append(newspaper.PublicationDate.ToString("yyyy-MM-dd")).Append(";")
                            .Append(newspaper.Publisher).Append(";");

                        sb.Append(sb.ToString().GetHashCode());

                        sw.WriteLine(sb);
                    }
                    else
                    {
                        var sb = new StringBuilder();
                        var patent = (Patent) document;
                        sb.Append("Patent").Append(";")
                            .Append(patent.ApplicationDate.ToString("yyyy-MM-dd")).Append(";")
                            .Append(patent.Country).Append(";")
                            .Append(patent.RegistrationNumber).Append(";")
                            .Append(patent.Name).Append(";")
                            .Append(patent.Annotation ?? "null").Append(";")
                            .Append(patent.PagesCount).Append(";")
                            .Append(patent.PublicationDate.ToString("yyyy-MM-dd")).Append(";");

                        for (var i = 0; i < patent.Inventors.Count; i++)
                        {
                            sb.Append(patent.Inventors[i].FirstName).Append(",");
                            sb.Append(patent.Inventors[i].LastName).Append(";");
                        }

                        sb.Append(sb.ToString().GetHashCode());

                        sw.WriteLine(sb);
                    }
                }
            }
        }
    }
}
