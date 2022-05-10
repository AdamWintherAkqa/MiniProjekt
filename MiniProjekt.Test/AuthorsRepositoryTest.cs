using Microsoft.EntityFrameworkCore;
using MiniProjekt.DAL;
using MiniProjekt.DAL.Database;
using MiniProjekt.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniProjekt.Test
{
    public class AuthorsRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly AuthorRepository _authorRepository;

        public AuthorsRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjectAuthors")
                .Options;

            _context = new(_options);

            _authorRepository = new AuthorRepository(_context);
        }

        [Fact]
        public async void SelectAllAuthors_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Author.Add(new() { AuthorId = 1, Name = "Author 1", IsAlive = true, Password = "abcd" });
            _context.Author.Add(new() { AuthorId = 2, Name = "Author 2", IsAlive = false, Password = "qwerty" });

            await _context.SaveChangesAsync();

            //act
            var result = await _authorRepository.GetAllAuthors();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllAuthors_ShouldReturnEmptyListOfAuthors_WhenNoAuthorExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            //act
            var result = await _authorRepository.GetAllAuthors();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectAuthorById_ShouldReturnAuthor_WhenAuthorExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            _context.Author.Add(new()
            {
                AuthorId = authorId,
                Name = "Palle Kalle",
                IsAlive = true,
                Password = "abcd"
            });

            await _context.SaveChangesAsync();

            //act
            var result = await _authorRepository.GetAuthorById(authorId);

            //assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.AuthorId);
        }

        [Fact]
        public async void SelectAuthorById_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            //act
            var result = await _authorRepository.GetAuthorById(1);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewAuthor_shouldAddnewIdToAuthor_WhenSavingToDatabase()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Author author = new()
            {
                Name = "George Martin R.R.",
                IsAlive = false,
                Password = "abcd"
            };

            //act
            var result = await _authorRepository.CreateAuthor(author);

            //assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(expectedNewId, result.AuthorId);
        }

        [Fact]
        public async void InsertNewAuthor_ShouldFailToAddNewAuthor_WhenAuthorIdAlreadyExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            Author author = new()
            {
                AuthorId = 1,
                Name = "author1",
                Password = "abcd",
                IsAlive = true
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            //act
            async Task action() => await _authorRepository.CreateAuthor(author);

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

        //    Author newAuthor = new()
        //    {
        //        AuthorId = authorId,
        //        Name = "George Georgesen",
        //        IsAlive = true,
        //        Password = "abcd"
        //    };

        //    _context.Author.Add(newAuthor);
        //    await _context.SaveChangesAsync();

        //    Author updateAuthor = new()
        //    {
        //        AuthorId = authorId,
        //        Name = "Updated Georgesen",
        //        IsAlive = true,
        //        Password = "abcd"
        //    };

        //    //act
        //    var result = await _authorRepository.
        //}

        [Fact]
        public async void DeleteAuthorById_ShouldReturnDeletedAuthor_WhenAuthorIsDeleted()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author newAuthor = new()
            {
                AuthorId = authorId,
                Name = "Anders",
                IsAlive = true,
                Password = "qwerty"
            };

            _context.Author.Add(newAuthor);
            await _context.SaveChangesAsync();

            //act
            var result = await _authorRepository.DeleteAuthorById(authorId);
            var author = await _authorRepository.GetAuthorById(authorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.AuthorId);
        }

        [Fact]
        public async void DeleteAuthorById_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            //act
            var result = await _authorRepository.DeleteAuthorById(1);

            //assert
            Assert.Null(result);
        }
    }
}
