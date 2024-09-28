namespace ODataBookStore.Models
{
    public static class DataSource
    {
        private static IList<Book> listBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if (listBooks != null)
            {
                return listBooks;
            }

            listBooks = new List<Book>();
            Book book = new Book
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Vuhuy",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book,
                    Email = "info@addison-wesley.com"
                }
            };
            listBooks.Add(book);

            Book book1 = new Book
            {
                Id = 2,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C# 5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Addison-Wesley",
                    Category = Category.Book,
                    Email = "info@addison-wesley.com"
                }
            };
            listBooks.Add(book1);

            return listBooks;
        }
    }
}
