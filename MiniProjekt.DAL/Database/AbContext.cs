using Microsoft.EntityFrameworkCore;
using MiniProjekt.DAL.Database.Models;

namespace MiniProjekt.DAL.Database
{
    public class AbContext : DbContext
    {
        public AbContext() { }
        public AbContext(DbContextOptions<AbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CPH00151\MSSQLSERVER01;Database=flemmingersej;Trusted_Connection=True;");
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Author>().HasData(
        //        new Author()
        //        {
        //            AuthorId = 1,
        //            Name = "George Martin",
        //            IsAlive = true,
        //            Password = "abcd"
        //        },
        //        new Author()
        //        {
        //            AuthorId = 1,
        //            Name = "Lewis Carol",
        //            IsAlive = false,
        //            Password = "efgh"
        //        }
        //    );

        //}
    }
}
