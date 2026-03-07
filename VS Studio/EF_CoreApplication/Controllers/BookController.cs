using EF_CoreApplication.Models;
using EF_CoreApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EF_CoreApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repo;

        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        // List - Display all books
        public IActionResult List()
        {
            var books = _repo.GetAllBooks();
            return View(books);
        }

        // Details - Display a single book
        public IActionResult Details(int id)
        {
            var book = _repo.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // Create - GET
        public IActionResult Create()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _repo.AddBook(book);
                return RedirectToAction(nameof(List));
            }
            return View(book);
        }

        // Delete - GET
        public IActionResult Delete(int id)
        {
            var book = _repo.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteBook(id);
            return RedirectToAction(nameof(List));
        }
    }
}
