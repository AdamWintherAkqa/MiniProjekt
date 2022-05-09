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
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<int> CreateBook(Book book);
        Task<int> DeleteBookById(int id);
    }
    public class BookRepository : IBookRepository
    {
        private readonly AbContext context;
        public BookRepository(AbContext _context)
        {
            context = _context;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await context.Book.ToListAsync();
        }
        public async Task<Book> GetBookById(int id)
        {
            return await context.Book.FirstOrDefaultAsync((bookObj) => bookObj.BookId == id);
        }
        public async Task<int> CreateBook(Book book)
        {
            context.Book.Add(book);
            return await context.SaveChangesAsync();
        }
        public async Task<int> DeleteBookById(int id)
        {
            Book item = context.Book.Where(item => item.BookId == id).Single();
            if (item != null)
            {
                context.Book.Remove(item);
                return await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Book not found");
            }
        }
    }
}
