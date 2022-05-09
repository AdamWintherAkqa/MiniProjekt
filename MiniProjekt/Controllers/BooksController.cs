using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProjekt.DAL;
using MiniProjekt.DAL.Database;
using MiniProjekt.DAL.Database.Models;

namespace MiniProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository context;

        public BooksController(IBookRepository _context) //dependency injection
        {
            context = _context ?? throw new ArgumentNullException(nameof(_context));
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetBook()
        {
            try
            {
                List<Book> result = await context.GetAllBooks();
                if (result == null)
                {
                    return StatusCode(500);
                }

                if (result.Count == 0)
                {
                    return NoContent();
                }

                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return (ActionResult)StatusCode(500, ex);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            
            try
            {
                var book = context.GetBookById(id);

                if (book == null)
                {
                    return NotFound();
                }

                return await book;
            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }

            
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.BookId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            try
            {
                await context.CreateBook(book);

                return CreatedAtAction("GetBook", new { id = book.BookId }, book);
            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }
            
        }

        //// DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var response = await context.DeleteBookById(id);
                if (response != 0)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return (ActionResult) BadRequest(ex.Message);
            }
            
        }

        //private bool BookExists(int id)
        //{
        //    return _context.Book.Any(e => e.BookId == id);
        //}
    }
}
