﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
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
    /// <summary>
    /// Jeg vil gerne kunne teste API laget.
    /// DVS. vi kommer ikke til at benytte database mm.
    /// DVS. vi kan ikke teste det vi gerne vil.
    /// 
    /// AAA
    /// ARRANGE - DEFINITION ( VARIABLER MM)
    /// ACT     - RESULT ( INVOKE VORES METODER )
    /// ASSERT  - SAMMENLIGNE 2 VÆRDIER
    ///         - Returnere jeg det rigtige osv.
    /// </summary>
    public class AuthorsControllerTest
    {
        //SUT - system under test
        private readonly AuthorsController _sut;
        //Mock er til for at simulere data
        private readonly Mock<IAuthorRepository> _authorRepo = new Mock<IAuthorRepository>();

        public AuthorsControllerTest()
        {
            _sut = new AuthorsController(_authorRepo.Object);
        }

        //decorates a method in xunit
        [Fact]
        public async void Get_ShouldReturnAllAuthors200()
        {
            //arrange
            List<Author> authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "Author 1", IsAlive  = true, Password = "123" },
                new Author { AuthorId = 2, Name = "Author 2", IsAlive  = false, Password = "abc" },
                new Author { AuthorId = 3, Name = "Author 3", IsAlive  = true, Password = "dfg" },
            };

            _authorRepo.Setup(authorObj => authorObj.GetAllAuthors()).ReturnsAsync(authors);

            //act
            var result = await _sut.GetAllAuthors();    //Simulerer at vi invoker en metode med masser af data
            var status = (IStatusCodeActionResult)result;

            //assert
            Assert.Equal(200, status.StatusCode);
        }

        [Fact]
        public async void Get_ShouldReturnAllAuthors500()
        {
            //arrange
            _authorRepo.Setup()
        }
    }
