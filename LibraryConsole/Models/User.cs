using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsole.Models
{
    class User
    {
        public User() { }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<string> BorrowedBooks { get; set; } = new List<string>();
    }
}
