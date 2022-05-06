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
        Author GetAuthorById(int id);
        void CreateAuthor(Author author);
        Task<int> DeleteAuthorById(int id);

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
        public Author GetAuthorById(int id)
        {
            return context.Author.FirstOrDefault((authorObj) => authorObj.AuthorId == id);
        }
        public void CreateAuthor(Author author)
        {
            context.Author.Add(author);
            context.SaveChanges();
        }
        public async Task<int> DeleteAuthorById(int id)
        {
            Author item = context.Author.Where(item => item.AuthorId == id).Single();
            if (item != null)
            {
                context.Author.Remove(item);
                return await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Author not found");
            }
            
        }
    }
}
