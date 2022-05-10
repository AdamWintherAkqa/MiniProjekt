using Microsoft.EntityFrameworkCore;
using MiniProjekt.DAL.Database;
using MiniProjekt.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt.DAL
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> CreateAuthor(Author author);
        Task<Author> DeleteAuthorById(int id);

    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AbContext context;
        public AuthorRepository(AbContext _context)
        {
            context = _context;
        }
        public async Task<List<Author>> GetAllAuthors()
        {           
            return await context.Author.ToListAsync();
        }
        public async Task<Author> GetAuthorById(int id)
        {
            return await context.Author.FirstOrDefaultAsync((authorObj) => authorObj.AuthorId == id);
        }
        public async Task<Author> CreateAuthor(Author author)
        {
            context.Author.Add(author);
            await context.SaveChangesAsync();

            return author;
        }
        public async Task<Author> DeleteAuthorById(int id)
        {
            try
            {
                Author item = context.Author.Where(item => item.AuthorId == id).Single();
                if (item != null)
                {
                    context.Author.Remove(item);
                    await context.SaveChangesAsync();
                    return item;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            
            
            
        }
    }
}
