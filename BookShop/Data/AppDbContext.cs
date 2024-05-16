using BookShop.Areas.Identity.Data;
using BookShop.Migrations;
using BookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BookShop.Data
{
    public class AppDbContext : IdentityDbContext<BookShopUser>
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<BookGenre>().HasKey(bg => new
        //    {
        //        bg.BookId,
        //        bg.GenreId
        //    });
        //    modelBuilder.Entity<BookGenre>().HasOne(b => b.Book).WithMany(bg => bg.BookGenres).HasForeignKey(b => b.BookId);
        //    modelBuilder.Entity<BookGenre>().HasOne(b => b.Genre).WithMany(bg => bg.BookGenres).HasForeignKey(b => b.GenreId);

        //}

        //internal async Task<string?> GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

