
using BookShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int? YearPublished { get; set; }
        [Required(ErrorMessage = "Number of pages is required")]
        public int? NumPages { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Publisher is required")]
        public string? Publisher { get; set; }
        public string? FrontPage { get; set; }
        [Required(ErrorMessage = "DownloadUrl is required")]
        public string? DownloadUrl { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<BookGenre>? BookGenres { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserBook>? UserBooks { get; set; }
       
    }
}
