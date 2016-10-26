using System;

namespace Library
{
    public static class Catalog
    {
        private static IDocument[] documents = new IDocument[0];

        // 1. Добавление записей в каталог.
        public static void Add(Document newDocument)
        {
            var arrayHaveGaps = false;

            var i = 0;

            for (; i < documents.Length; i++)
            {
                if (documents[i] != null)
                {
                    continue;
                }
                arrayHaveGaps = true;
                break;
            }

            if (arrayHaveGaps)
            {
                documents[i] = newDocument;
            }
            else
            {
                Array.Resize(ref documents, documents.Length + 1);
                documents[documents.Length - 1] = newDocument;
            }
        }

        // 2. Удаление записей из каталога.
        public static void Remove(int index)
        {
            if (index + 1 == documents.Length)
            {
                Array.Resize(ref documents, documents.Length - 1);
            }
            else
            {
                documents[index] = null;
                for (var i = index; i < documents.Length - 1; i++)
                {
                    documents[i] = documents[i + 1];
                }
                Array.Resize(ref documents, documents.Length - 1);
            }
        }

        // 3. Просмотр каталога.
        /* 
            Так как у нас проект типа Class library, то вывод какой-либо информации реализовать
            не возможно (например в консоль или в какой-нибудь контрол на форме). Так что этот метод 
            просто возвращает содержимое каталога. Вывод реализован в классе-эмуляторе LibraryEmulator.
        */
        public static IDocument[] GetCatalogContent()
        {
            return documents;
        }
        
        // 4. Поиск по названию.
        public static IDocument[] FindByName(string name)
        {
            var findedDocsCount = 0;
            foreach (var document in documents)
            {
                if (document.Name == name)
                {
                    findedDocsCount++;
                }
            }

            var findedDocuments = new IDocument[findedDocsCount];
            for (var i = 0; i < findedDocuments.Length; i++)
            {
                foreach (var document in documents)
                {
                    if (document.Name == name)
                    {
                        findedDocuments[i] = document;
                    }
                }
                
            }

            return findedDocuments;
        }

        // 5. Сортировка по году выпуска в прямом порядке.
        // 8. Группировка записей по годам издания.
        /*
            Так как тут написано "записей", а не книг, то я буду группировать все типы (в том числе и патенты у которых нет
            такого понятия как "год издания").
        */
        public static IDocument[] GetSortedContentByPublicationDateAsc()
        {
            var sortedArray = new Document[documents.Length];
            Array.Copy(documents, sortedArray, documents.Length);
            Array.Sort(sortedArray);
            return sortedArray;
        }

        // 5. Сортировка по году выпуска в обратном порядке.
        public static IDocument[] GetSortedContentByPublicationDateDesc()
        {
            var sortedArray = GetSortedContentByPublicationDateAsc();
            Array.Reverse(sortedArray);
            return sortedArray;
        }

        // 6. Поиск всех книг данного автора (в том числе, тех, у которых он является соавтором).
        public static Book[] GetBooksByAuthor(Person suitableAuthor)
        {
            var books = GetBooks();
            var suitableBooksCount = 0;
            foreach (var book in books)
            {
                foreach (var author in book.Authors)
                {
                    if (author.FirstName == suitableAuthor.FirstName && author.LastName == suitableAuthor.LastName)
                    {
                        suitableBooksCount++;
                    }
                }
            }

            var suitableBooks = new Book[suitableBooksCount];
            var currentSuitableBookIndex = 0;
            foreach (var book in books)
            {
                foreach (var author in book.Authors)
                {
                    if (author.FirstName == suitableAuthor.FirstName && author.LastName == suitableAuthor.LastName)
                    {
                        suitableBooks[currentSuitableBookIndex] = book;
                        currentSuitableBookIndex++;
                    }
                }
            }

            return suitableBooks;
        }

        private static Book[] GetBooks()
        {
            var booksCount = 0;
            foreach (var document in documents)
            {
                if (document is Book)
                {
                    booksCount++;
                }
            }

            var books = new Book[booksCount];
            var currentBookIndex = 0;
            foreach (var document in documents)
            {
                if (document is Book)
                {
                    books[currentBookIndex] = (Book)document;
                    currentBookIndex++;
                }
            }

            return books;
        }

        // 7. Вывод всех книг, название издательства которых начинаются с заданного набора символов, с группировкой по издательству.
        public static Book[] GetBooksByPartOfPublisher(string partOfPublisherName)
        {
            var books = GetBooks();
            var suitableBooksCount = 0;
            foreach (var book in books)
            {
                if (book.Publisher.StartsWith(partOfPublisherName))
                {
                    suitableBooksCount++;
                }
            }

            var suitableBooks = new Book[suitableBooksCount];
            var currentBookIndex = 0;
            foreach (var book in books)
            {
                if (book.Publisher.StartsWith(partOfPublisherName))
                {
                    suitableBooks[currentBookIndex] = book;
                    currentBookIndex++;
                }
            }

            return GroupBooksByPublisher(suitableBooks);
        }

        private static Book[] GroupBooksByPublisher(Book[] books)
        {
            var publishers = new string[books.Length];
            for (var i = 0; i < books.Length; i++)
            {
                publishers[i] = books[i].Publisher;
            }
            var uniquePublishers = GetUniquePublishers(publishers);

            var groupedBooks = new Book[books.Length];
            for (var i = 0; i < uniquePublishers.Length; i++)
            {
                for (var j = 0; j < books.Length; j++)
                {
                    if (books[j].Publisher == uniquePublishers[i])
                    {
                        for (var k = 0; k < groupedBooks.Length; k++)
                        {
                            if (groupedBooks[k] == null)
                            {
                                groupedBooks[k] = books[j];
                                break;
                            }
                        }
                    }
                }
            }

            return groupedBooks;
        }

        private static string[] GetUniquePublishers(string[] publishers)
        {
            var doublesCount = 0;
            for (var i = 0; i < publishers.Length; i++)
            {
                for (var j = i + 1; j < publishers.Length; j++)
                {
                    if (!string.IsNullOrEmpty(publishers[i]) && publishers[i] == publishers[j])
                    {
                        publishers[j] = null;
                        doublesCount++;
                    }
                }
            }

            var uniquePublishers = new string[publishers.Length - doublesCount];
            var uniquePublisherIndex = 0;
            foreach (var publisher in publishers)
            {
                if (string.IsNullOrEmpty(publisher)) continue;
                uniquePublishers[uniquePublisherIndex] = publisher;
                uniquePublisherIndex++;
            }

            return uniquePublishers;
        }

        // 8. Группировка записей по годам издания.
        
    }
}
