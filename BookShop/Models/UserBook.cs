using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class UserBook
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        [ForeignKey("AppUserInfo")]
        public string AppUser { get; set; }
        public AppUser? AppUserInfo { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
