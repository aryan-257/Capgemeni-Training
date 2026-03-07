using EF_CoreApplication.Models;

namespace EF_CoreApplication.Repository
{
    public class MemoryBookRepository : IBookRepository
    {
        private readonly Dictionary<int, Book> _books = new Dictionary<int, Book>();

        public MemoryBookRepository()
        {
            // Initialize with 3 sample books
            _books.Add(1, new Book { BookId = 1, Title = "Clean Code", Author = "Robert C. Martin", Price = 45.99m });
            _books.Add(2, new Book { BookId = 2, Title = "Design Patterns", Author = "GoF", Price = 54.99m });
            _books.Add(3, new Book { BookId = 3, Title = "Refactoring", Author = "Martin Fowler", Price = 49.99m });
        }

        public List<Book> GetAllBooks()
        {
            return _books.Values.ToList();
        }

        public Book? GetBookById(int id)
        {
            return _books.ContainsKey(id) ? _books[id] : null;
        }

        public void AddBook(Book book)
        {
            if (book.BookId == 0)
            {
                book.BookId = _books.Count > 0 ? _books.Keys.Max() + 1 : 1;
            }
            _books[book.BookId] = book;
        }

        public void DeleteBook(int id)
        {
            if (_books.ContainsKey(id))
            {
                _books.Remove(id);
            }
        }
    }
}
