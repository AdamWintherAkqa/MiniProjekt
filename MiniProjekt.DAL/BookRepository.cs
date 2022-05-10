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
        Task<Book> CreateBook(Book book);
        Task<Book> DeleteBookById(int id);
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
        public async Task<Book> CreateBook(Book book)
        {
            context.Book.Add(book);
            await context.SaveChangesAsync();
            return book;
        }
        public async Task<Book> DeleteBookById(int id)
        {
            try
            {
                Book item = context.Book.Where(item => item.BookId == id).Single();
                if (item != null)
                {
                    context.Book.Remove(item);
                    await context.SaveChangesAsync();
                    return item;
                }
                else
                {
                    throw new Exception("Book not found");
                }
            }
            catch
            {
                return null;
            }
            
        }
    }
}
