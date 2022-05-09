using Microsoft.AspNetCore.Mvc.Infrastructure;
using MiniProjekt.Controllers;
using MiniProjekt.DAL;
using MiniProjekt.DAL.Database.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniProjekt.Test
{
    public class BooksControllerTest
    {
        private readonly BooksController _sut;
        private readonly Mock<IBookRepository> _bookRepo = new Mock<IBookRepository>();

        public BooksControllerTest()
        {
            _sut = new BooksController(_bookRepo.Object);
        }

        [Fact]
        public async void Get_ShouldReturnAllBooks200()
        {
            // Arrange
            List<Book> books = new List<Book>();
            {
                books.Add(new Book { BookId = 1, BookTitle = "Book 1", Pages = 100, Binding = true, ReleaseYear = new DateTime(2019, 1, 1) });
                books.Add(new Book { BookId = 2, BookTitle = "Book 2", Pages = 110, Binding = false, ReleaseYear = new DateTime(1996, 1, 1) });
            };

            _bookRepo.Setup(x => x.GetAllBooks()).ReturnsAsync(books);

            //act
            var result = await _sut.GetBook();
            var status = (IStatusCodeActionResult)result;

            //assert
            Assert.Equal(200, status.StatusCode);
        }

        [Fact]
        public async void getAllBooks_ListNotExisting()
        {
            // Arrange
            _bookRepo.Setup(x => x.GetAllBooks()).ReturnsAsync(() => null);

            //act
            var result = await _sut.GetBook();

            //assert
            var statusCodeREsult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeREsult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoBookExists()
        {
            // Arrange
            List<Book> books = new();
            _bookRepo.Setup(x => x.GetAllBooks()).ReturnsAsync(books);

            //act
            var result = await _sut.GetBook();

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNoNullIsReturnedFromService()
        {
            // Arrange
            _bookRepo.Setup(x => x.GetAllBooks()).ReturnsAsync(() => null);

            //act
            var result = await _sut.GetBook();

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _bookRepo.Setup(x => x.GetAllBooks()).ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //act
            var result = await _sut.GetBook();

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
