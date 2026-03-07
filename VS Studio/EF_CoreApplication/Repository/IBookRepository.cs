using EF_CoreApplication.Models;

namespace EF_CoreApplication.Repository
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        void AddBook(Book book);
        void DeleteBook(int id);
    }
}
