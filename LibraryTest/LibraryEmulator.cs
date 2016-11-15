using System;
using Library;

namespace LibraryTest
{
    internal sealed class LibraryEmulator
    {
        public static void Main()
        {

            Initialize();

            // 3. Просмотр каталога.
            var catalog = Catalog.GetCatalogContent();
            Show(catalog);

            Console.WriteLine("--------------------");

            // 4. Поиск по названию.
            catalog = Catalog.FindByName("Телефон");
            Show(catalog);

            Console.WriteLine("--------------------");

            // 5. Сортировка по году выпуска в прямом порядке.
            catalog = Catalog.GetSortedContentByPublicationDateAsc();
            Show(catalog);

            Console.WriteLine("--------------------");

            // 5. Сортировка по году выпуска в прямом порядке.
            catalog = Catalog.GetSortedContentByPublicationDateDesc();
            Show(catalog);

            Console.WriteLine("--------------------");

            // 6. Поиск всех книг данного автора.
            var books = Catalog.GetBooksByAuthor(new Person("Лев", "Толстой"));
            Show(books);

            Console.WriteLine("--------------------");

            // 7. Вывод всех книг, название издательства которых начинаются с заданного набора символов, с группировкой по издательству
            books = Catalog.GetBooksByPartOfPublisher("Изд");
            Show(books);

            Console.WriteLine("--------------------");

            // 8. Группировка записей по годам издания.
            catalog = Catalog.GetSortedContentByPublicationDateAsc();
            foreach (var document in catalog)
            {
                Console.WriteLine("{0} - {1}", document.PublicationDate, document.Name);
            }

            Console.ReadKey();
        }

        private static void Initialize()
        {
            var book1 = new Book("Война и мир", 500, "Москва", "Издательство", "ISBN-1",
                new[] { new Person("Лев", "Толстой"), }, DateTime.Today);
            var book2 = new Book("Бойцовский клуб", 500, "Москва", "Другое издательство", "ISBN-2",
                new[] { new Person("Чак", "Паланик"), new Person("Лев", "Толстой"),  }, DateTime.Today.AddDays(-1));
            var book3 = new Book("Ведьмак", 500, "Москва", "Издательство", "ISSN-3",
                new[] { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));
            var book4 = new Book("Хлеб с ветчиной", 500, "Москва", "Издательство", "ISSN-3",
                new[] { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));

            // 1. Добавление записей в каталог.
            Catalog.Add(book1);
            Catalog.Add(book2);
            Catalog.Add(book3);
            Catalog.Add(book4);

            // 2. Удаление записей из каталога.
            Catalog.Remove(2);

            var newspaper1 = new Newspaper("Коммерсант", 30, "Москва", "Издательство", "ISSN-1", 1,
                DateTime.Today);
            var newspaper2 = new Newspaper("Новая газета", 30, "Москва", "Издательство", "ISSN-2", 1,
                DateTime.Today.AddDays(-1));
            var newspaper3 = new Newspaper("Московский комсомолец", 30, "Москва", "Издательство", "ISSN-3", 1,
                DateTime.Today.AddYears(-1));

            // 1. Добавление записей в каталог.
            Catalog.Add(newspaper1);
            Catalog.Add(newspaper2);
            Catalog.Add(newspaper3);

            var patent1 = new Patent("Телефон", 50, "T-1", DateTime.Today, "Россия", new[] { new Person("Иван", "Иванов") },
                DateTime.Today);
            var patent2 = new Patent("Компьютер", 50, "К-1", DateTime.Today, "Россия", new[] { new Person("Иван", "Иванов") },
                DateTime.Today.AddMonths(1));
            var patent3 = new Patent("Телевизор", 50, "Т-2", DateTime.Today, "Россия", new[] { new Person("Иван", "Иванов") },
                DateTime.Today.AddYears(2));
            var patent4 = new Patent("Телефон", 50, "T-2", DateTime.Today, "Россия", new[] { new Person("Иван", "Иванов") },
                DateTime.Today);

            // 1. Добавление записей в каталог.
            Catalog.Add(patent1);
            Catalog.Add(patent2);
            Catalog.Add(patent3);
            Catalog.Add(patent4);
        }

        private static void Show(Document[] catalog)
        {
            foreach (var document in catalog)
            {
                Console.WriteLine(document);
            }
        }
    }
}
