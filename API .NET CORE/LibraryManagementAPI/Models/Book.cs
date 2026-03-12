using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Range(1000, 2024)]
        public int PublishedYear { get; set; }

        public int? LibraryCardId { get; set; }

        public LibraryCard LibraryCard { get; set; }
    }
}
