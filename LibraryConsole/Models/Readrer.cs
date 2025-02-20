using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsole.Models
{
    public class Reader
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int BorrowedBooksCount { get; set; }
        public List<Book> BorrowedBooks { get; set; } = new List<Book>();
    }

}
