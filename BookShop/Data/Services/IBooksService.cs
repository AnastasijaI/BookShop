﻿using BookShop.Models;

namespace BookShop.Data.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        void Add(Book book);
        Book Update(int id, Book book);
        //Task AddAsync(Book book);
        //Task<Book> UpdateAsync(int id, Book book);
        Task DeleteAsync(int id);
        Task<Book> GetLastBook();
        Task<Book> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<IEnumerable<Book>> GetBooksByAuthorId(int id);
        Task<IEnumerable<Book>> GetBooksByGenreId(int id);
  
    }
}
