using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt.DAL.Database.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Pages { get; set; }
        public bool Binding { get; set; }
        public DateTime ReleaseYear { get; set; }
    }
}
