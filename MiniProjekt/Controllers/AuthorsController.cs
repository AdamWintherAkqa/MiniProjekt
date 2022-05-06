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
    /// <summary>
    /// Handles all requests for the user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository context;

        public AuthorsController(IAuthorRepository _context)
        {
            context = _context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {
            try
            {
                return Ok(await context.GetAllAuthors()); // Ok kan typecast 99% af alt kode whoo!
            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }
        }

        //GET: api/Authors/5
        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                var author = context.GetAuthorById(id);

                if (author == null)
                {
                    return NotFound();
                }

                return author;
            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }
        }

        //// PUT: api/Authors/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAuthor(int id, Author author)
        //{
        //    if (id != author.AuthorId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(author).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AuthorExists(id))
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

        //// POST: api/Authors
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            try
            {
                await context.CreateAuthor(author);

                return CreatedAtAction("GetBook", new { id = author.AuthorId }, author);
            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }
        }

        //// DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var response = await context.DeleteAuthorById(id);
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
                return (ActionResult)BadRequest(ex.Message);
            }
        }

        //private bool AuthorExists(int id)
        //{
        //    return _context.Author.Any(e => e.AuthorId == id);
        //}
    }
}
