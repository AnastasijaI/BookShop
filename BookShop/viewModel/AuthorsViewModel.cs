using BookShop.Models;

namespace BookShop.viewModel
{
    public class AuthorsViewModel
    {
        public IEnumerable<Author>? Authors { get; set; }
        public string SearchByName { get; set; }
    }
}
