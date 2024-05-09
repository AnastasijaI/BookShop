using BookShop.Data.Base;
using BookShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Author:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "First Name is required")]
       // [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Last Name is required")]
        // [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "BirthDate is required")]
        public DateTime? BirthDate { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Nationality is required")]
        public string? Nationality { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        // Навигационо својство за книгите на авторот
        public ICollection<Book>? Books { get; set; }

    }
}
