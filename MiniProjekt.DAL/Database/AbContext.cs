using Microsoft.EntityFrameworkCore;
using MiniProjekt.DAL.Database.Models;

namespace MiniProjekt.DAL.Database
{
    public class AbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CPH00151\MSSQLSERVER01;Database=flemmingersej;Trusted_Connection=True;");
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
