using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class LibraryCard
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"LC-\d{5}")]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string MemberName { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
