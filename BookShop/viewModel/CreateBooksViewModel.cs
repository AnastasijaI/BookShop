
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookShop.Models;

namespace BookShop.viewModel
{
    public class CreateBooksViewModel
    {
        //[Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Publication year")]
        //[Required(ErrorMessage = "YearPublished is required")]
        public int? YearPublished { get; set; }

        [Display(Name = "Number of Pages")]
        //[Required(ErrorMessage = "NumPages is required")]
        public int? NumPages { get; set; }

        [Display(Name = "Description")]
        //[Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Display(Name = "Publisher")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        //[Required(ErrorMessage = "Publisher is required")]
        public string? Publisher { get; set; }

        [Display(Name = "Front Page")]
        //[Required(ErrorMessage = "FrontPage is required")]
        public string? FrontPage { get; set; }

        [Display(Name = "Download here")]
        //[Required(ErrorMessage = "DownloadUrl is required")]
        public string? DownloadUrl { get; set; }
        public IEnumerable<Author>? Authors { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
    }
}

