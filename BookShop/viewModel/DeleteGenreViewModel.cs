using BookShop.Models;

namespace BookShop.viewModel
{
    public class DeleteGenreViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int genreId { get; set; }
    }
}
