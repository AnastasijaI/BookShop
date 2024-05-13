using BookShop.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookShop.viewModel
{
    public class BookReviewViewModel
    {
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        [Required(ErrorMessage = "AppUser is required")]
        public string AppUser { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Raiting is required")]
        public int? Rating { get; set; }
    }
}
