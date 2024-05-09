using BookShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Data.Services
{
    public interface IBooksGenresService
    {
        Task<IEnumerable<BookGenre>> GetAll();
        void Add(BookGenre bookGenre);
        Task<BookGenre> UpdateAsync(BookGenre bookGenre);
        bool Delete(BookGenre bookGenre);
        bool Save();
    }
}
