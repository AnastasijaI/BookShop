using BookShop.Models;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();


                //Author
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            FirstName = "James",
                            LastName = "Clear",
                            BirthDate = new DateTime(1986, 1, 22),
                            Nationality = "American",
                            Gender = "Male"
                        },
                        new Author()
                        {
                            FirstName = "Stephen",
                            LastName = "King",
                            BirthDate = new DateTime(1947, 9, 21),
                            Nationality = "American",
                            Gender = "Male"
                        },
                        new Author()
                        {
                            FirstName = "Joseph",
                            LastName = "Murphy",
                            BirthDate = new DateTime(1898, 5, 20),
                            Nationality = "American",
                            Gender = "Male"
                        },
                        new Author()
                        {
                            FirstName = "Fyodor",
                            LastName = "Dostoevsky",
                            BirthDate = new DateTime(1821, 11, 11),
                            Nationality = "Russian",
                            Gender = "Male"
                        }
                    });
                    context.SaveChanges();

                }
                //Book
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Title = "Atomic Habits",
                            YearPublished = 2018,
                            NumPages = 320,
                            Description = "In 'Atomic Habits,' author James Clear presents a transformative framework for building and sustaining positive habits. Through practical strategies and scientific insights, Clear explores how small changes can lead to remarkable results, empowering readers to create lasting habits that align with their goals and aspirations. Whether aiming for personal growth, productivity, or success, this book offers actionable advice to make meaningful changes in behavior and lifestyle.",
                            Publisher = "Penguin Random House",
                            FrontPage = "https://is1-ssl.mzstatic.com/image/thumb/Publication116/v4/f8/2c/e5/f82ce57d-c12b-0a16-f113-e7f892ae033f/9780735211308.d.jpg/1200x1200bb.png",
                            DownloadUrl = "https://onlinebooks.library.upenn.edu/lists.html",
                            AuthorId = 1
                        },
                        new Book()
                        {
                            Title = "The Waste Lands",
                            YearPublished = 1991,
                            NumPages = 512,
                            Description = "It is the third book of the Dark Tower series. The original limited edition hardcover featuring full-color illustrations by Ned Dameron was published in 1991 by Grant. The book was reissued in 2003 to coincide with the publication of The Dark Tower V: Wolves of the Calla. The book derives its title from the T. S. Eliot 1922 poem The Waste Land, several lines of which are reprinted in the opening pages.",
                            Publisher = "Grant",
                            FrontPage = "https://s.s-bol.com/imgbase0/imagebase3/large/FC/3/5/7/8/1001004011438753.jpg",
                            DownloadUrl = "https://ia601405.us.archive.org/6/items/stephen-king_202206/Stephen%20King%20-%20Dark%20Tower%2003%20-%20The%20Waste%20Lands.pdf",
                            AuthorId = 2
                        },
                        new Book()
                        {
                            Title = "The power of your subconscious mind",
                            YearPublished = 1993,
                            NumPages = 224,
                            Description = "'The Power of Your Subconscious Mind,' Joseph Murphy explores the potential of the subconscious mind to transform lives. Through anecdotes, practical techniques, and spiritual wisdom, Murphy illustrates how harnessing the power of the subconscious can lead to personal growth, success, and fulfillment. This timeless classic offers insights into the inner workings of the mind and provides guidance on tapping into its unlimited potential for positive change.",
                            Publisher = "New York; London: Pocket Books",
                            FrontPage = "https://baazimagess3.s3-ap-southeast-1.amazonaws.com/product_media/9788172345662/md_9788172345662.jpg",
                            DownloadUrl = "https://mc2method.org/books/ThePowerofYourSubconsciousMind.pdf",
                            AuthorId = 3
                        },
                        new Book()
                        {
                            Title = "Crime and Punishment",
                            YearPublished = 1866,
                            NumPages = 671,
                            Description = "In Dostoevsky's psychological thriller, 'Crime and Punishment,' follow the story of Rodion Raskolnikov, a destitute student who commits a heinous crime out of a twisted ideology. Set in the gritty streets of St. Petersburg, the novel delves into themes of morality, guilt, and redemption as Raskolnikov grapples with the consequences of his actions.",
                            Publisher = "The Russian Messenger",
                            FrontPage = "https://images.thenile.io/r1000/9780451530066.jpg",
                            DownloadUrl = "https://archive.org/details/crime-and-punishment-fyodor-dostoyevsky-pdf",
                            AuthorId = 4
                        }
                    });
                    context.SaveChanges();
                }
                //Genre
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(new List<Genre>()
                    {
                        new Genre()
                        {
                            GenreName = "Self-Help"
                        },
                        new Genre()
                        {
                            GenreName = "Horror"
                        },
                        new Genre()
                        {
                            GenreName = "Self-help, Non-Fiction"
                        },
                        new Genre()
                        {
                            GenreName = "Novel, Psychological Drama"
                        }
                    });
                    context.SaveChanges();
                }
                //BookGenre
                if (!context.BookGenres.Any())
                {
                    context.BookGenres.AddRange(new List<BookGenre>()
                    {
                        new BookGenre()
                        {
                            BookId=1,
                            GenreId=1
                        },
                        new BookGenre()
                        {
                            BookId=2,
                            GenreId=2
                        },
                        new BookGenre()
                        {
                            BookId=3,
                            GenreId=3
                        },
                        new BookGenre()
                        {
                            BookId=4,
                            GenreId=4
                        }
                    });
                    context.SaveChanges();
                }
                //Review
                if (!context.Reviews.Any())
                {
                    context.Reviews.AddRange(new List<Review>()
                    {
                        new Review()
                        {
                            BookId = 1,
                            AppUser = "user1",
                            Comment = "This book is amazing!",
                            Rating = 5
                        },
                        new Review()
                        {
                            BookId = 2,
                            AppUser = "user2",
                            Comment = "The book is interesting.",
                            Rating = 3
                        },
                        new Review()
                        {
                            BookId = 3,
                            AppUser = "user3",
                            Comment = "The book is quite instructive!",
                            Rating = 4
                        },
                        new Review()
                        {
                            BookId = 4,
                            AppUser = "user4",
                            Comment = "I am impressed!",
                            Rating = 5
                        }
                    });
                    context.SaveChanges();
                }
                //UserBook
                if (!context.UserBooks.Any())
                {
                    context.UserBooks.AddRange(new List<UserBook>()
                    {
                        new UserBook()
                        {
                            AppUser = "user1",
                            BookId = 1
                        },
                        new UserBook()
                        {
                            AppUser = "user2",
                            BookId = 2
                        },
                        new UserBook()
                        {
                            AppUser = "user3",
                            BookId = 3
                        },
                        new UserBook()
                        {
                            AppUser = "user4",
                            BookId = 4
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "app-admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
