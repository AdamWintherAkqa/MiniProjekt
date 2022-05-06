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
        Book GetBookById(int id);
        void CreateBook(Book book);
        Task DeleteBookById(int id);
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
        public Book GetBookById(int id)
        {
            return context.Book.FirstOrDefault((bookObj) => bookObj.BookId == id);
        }
        public void CreateBook(Book book)
        {
            context.Book.Add(book);
            context.SaveChanges();
        }
        public async Task DeleteBookById(int id)
        {
            Book item = context.Book.Where(item => item.BookId == id).Single();
            context.Remove(item);
            context.SaveChanges();
        }
    }
}
