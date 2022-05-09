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
    }
}
