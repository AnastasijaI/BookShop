using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book ? Book { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        [Required(ErrorMessage = "AppUser is required")]
        [ForeignKey("AppUserInfo")]
        public string AppUser { get; set; }
        public AppUser? AppUserInfo { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Raiting is required")]
        public int? Rating { get; set; }
    }
}

