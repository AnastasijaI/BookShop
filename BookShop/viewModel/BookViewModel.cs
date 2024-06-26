﻿using BookShop.Models;

namespace BookShop.viewModel
{
    public class BookViewModel
    {
        public IEnumerable<Book>? Books { get; set; }
        public IEnumerable<Author>? Authors { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
        public int AuthorSearchId { get; set; }
        public int GenreSearchId { get; set; }
        public string? SearchByName { get; set; }
    }
}
