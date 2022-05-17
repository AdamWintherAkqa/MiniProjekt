using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt.DAL.Database.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAlive { get; set; }
        public List<Book> Books { get; set; }
    }
}
