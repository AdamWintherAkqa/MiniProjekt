using Microsoft.EntityFrameworkCore;
using MiniProjekt.DAL;
using MiniProjekt.DAL.Database;
using MiniProjekt.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MiniProjekt.Test
{
    public class BooksRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly BookRepository _bookRepository;

        public BooksRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjectAuthors")
                .Options;

            _context = new(_options);

            _bookRepository = new BookRepository(_context);
        }

        [Fact]
        public async void SelectAllBooks_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Book.Add(new() { BookId = 1, BookTitle = "Game of Thrones 1", Binding = true, ReleaseYear = new DateTime(2019, 1, 1), Pages = 200 });
            _context.Book.Add(new() { BookId = 2, BookTitle = "Game of Thrones 2", Binding = true, ReleaseYear = new DateTime(2019, 1, 1), Pages = 200 });

            await _context.SaveChangesAsync();

            //act
            var result = await _bookRepository.GetAllBooks();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllBooks_ShouldReturnEmptyListOfAuthors_WhenNoAuthorExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            //act
            var result = await _bookRepository.GetAllBooks();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectBookById_ShouldReturnBook_WhenAuthorExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            int bookId = 1;

            _context.Book.Add(new()
            {
                BookId = 1,
                BookTitle = "Game of Thrones 1",
                Binding = true,
                ReleaseYear = new DateTime(2019, 1, 1),
                Pages = 200
            });

            await _context.SaveChangesAsync();

            //act
            var result = await _bookRepository.GetBookById(bookId);

            //assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(bookId, result.BookId);
        }

        [Fact]
        public async void SelectBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            //act
            var result = await _bookRepository.GetBookById(1);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewBook_shouldAddnewIdToBook_WhenSavingToDatabase()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Book book = new()
            {
                BookTitle = "Game of Thrones 1",
                Binding = true,
                ReleaseYear = new DateTime(2019, 1, 1),
                Pages = 200
            };

            //act
            var result = await _bookRepository.CreateBook(book);

            //assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(expectedNewId, result.BookId);
        }

        [Fact]
        public async void InsertNewBook_ShouldFailToAddNewBook_WhenBookIdAlreadyExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            Book book = new()
            {
                BookId = 1,
                BookTitle = "Game of Thrones 1",
                Binding = true,
                ReleaseYear = new DateTime(2019, 1, 1),
                Pages = 200
            };

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            //act
            async Task action() => await _bookRepository.CreateBook(book);

            //assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);

            Assert.Contains("An item with the same key has already been added.", ex.Message);
        }

        //[Fact]
        //public async void UpdateExistingAuthor_ShouldChangeValuesOnAuthor_WhenAuthorExists()
        //{
        //    //arrange
        //    await _context.Database.EnsureDeletedAsync();

        //    int authorId = 1;

        //    Book newAuthor = new()
        //    {
        //        AuthorId = authorId,
        //        Name = "George Georgesen",
        //        IsAlive = true,
        //        Password = "abcd"
        //    };

        //    _context.Book.Add(newAuthor);
        //    await _context.SaveChangesAsync();

        //    Book updateAuthor = new()
        //    {
        //        AuthorId = authorId,
        //        Name = "Updated Georgesen",
        //        IsAlive = true,
        //        Password = "abcd"
        //    };

        //    //act
        //    var result = await _bookRepository.
        //}

        [Fact]
        public async void DeleteBookById_ShouldReturnDeletedBook_WhenBookIsDeleted()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            int bookId = 1;

            Book newBook = new()
            {
                BookId = 1,
                BookTitle = "Game of Thrones 1",
                Binding = true,
                ReleaseYear = new DateTime(2019, 1, 1),
                Pages = 200
            };

            _context.Book.Add(newBook);
            await _context.SaveChangesAsync();

            //act
            var result = await _bookRepository.DeleteBookById(bookId);
            var author = await _bookRepository.GetBookById(bookId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(bookId, result.BookId);
        }

        [Fact]
        public async void DeleteBookById_ShouldReturnNull_WheBookDoesNotExist()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            //act
            var result = await _bookRepository.DeleteBookById(1);

            //assert
            Assert.Null(result);
        }
    }
}
