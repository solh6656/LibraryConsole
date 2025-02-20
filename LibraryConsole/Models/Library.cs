//using LibraryConsole.Models;
//using Newtonsoft.Json;

//class Library
//{
//    private List<Book> books = new List<Book>();
//    private List<User> users = new List<User>()
//            {
//                new User { Id = 1, Login = "ali", Password = "1234", FirstName = "Ali", LastName = "Valiyev", Age = 25 },
//                new User { Id = 2, Login = "tom", Password = "5678", FirstName = "Tom", LastName = "Johnson", Age = 30 }
//            }; 

//    private const string BooksFile = "books.json";
//    private const string UsersFile = "users.json";

//    public Library()
//    {
//        if (!File.Exists(BooksFile))
//        {
//            books = new List<Book>
//            {
//                new Book { Id = 1, Name = "C# Asoslari", Genre = "Dasturlash", Author = "Jon Doe", TotalCount = 5, AvailableCount = 5 },
//                new Book { Id = 2, Name = "Algoritmlar", Genre = "Dasturlash", Author = "Jane Smith", TotalCount = 3, AvailableCount = 3 },
//                new Book { Id = 3, Name = "Sherlar To‘plami", Genre = "Adabiyot", Author = "Abdulla Oripov", TotalCount = 4, AvailableCount = 4 }
//            };
//            SaveData();
//        }

//        if (!File.Exists(UsersFile))
//        { }
//            users = new List<User>()
//            {
//                new User { Id = 1, Login = "ali", Password = "1234", FirstName = "Ali", LastName = "Valiyev", Age = 25 },
//                new User { Id = 2, Login = "tom", Password = "5678", FirstName = "Tom", LastName = "Johnson", Age = 30 }
//            };
//            SaveData();
        
//    }
            

//    public void LoadData()
//    {
//        if (File.Exists(BooksFile))
//            books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(BooksFile)) ?? new List<Book>();
//        if (File.Exists(UsersFile))
//            users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(UsersFile)) ?? new List<User>();
//    }

//    public User AuthenticateUser(string login, string password)
//    {
//        return users.FirstOrDefault(u => u.Login == login && u.Password == password);
//    }

//    public void ShowBooks()
//    {
//        foreach (var book in books)
//        {
//            Console.WriteLine($"{book.Id}. {book.Name} - {book.Author} (Mavjud: {book.AvailableCount})");
//        }
//    }

//    public void GetBookInfo()
//    {
//        Console.Write("Kitob ID sini kiriting: ");
//        if (int.TryParse(Console.ReadLine(), out int id))
//        {
//            var book = books.FirstOrDefault(b => b.Id == id);
//            if (book != null)
//            {
//                Console.WriteLine($"Nomi: {book.Name}, Janri: {book.Genre}, Avtor: {book.Author}, Jami: {book.TotalCount}, Mavjud: {book.AvailableCount}");
//            }
//            else
//            {
//                Console.WriteLine("Kitob topilmadi!");
//            }
//        }
//    }

//    public void BorrowBook(User user)
//    {
//        Console.Write("Kitob ID sini kiriting: ");
//        if (int.TryParse(Console.ReadLine(), out int id))
//        {
//            var book = books.FirstOrDefault(b => b.Id == id);
//            if (book != null && book.AvailableCount > 0)
//            {
//                book.AvailableCount--;
//                book.Borrowers.Add(user.FirstName);
//                user.BorrowedBooks.Add(book.Name);
//                SaveData();
//                Console.WriteLine($"{book.Name} olindi!");
//            }
//            else
//            {
//                Console.WriteLine("Kitob mavjud emas!");
//            }
//        }
//    }

//    public void ReturnBook(User user)
//    {
//        Console.Write("Qaytariladigan kitob nomini kiriting: ");
//        string bookName = Console.ReadLine();
//        var book = books.FirstOrDefault(b => b.Name.Equals(bookName, StringComparison.OrdinalIgnoreCase));
//        if (book != null && user.BorrowedBooks.Contains(book.Name))
//        {
//            book.AvailableCount++;
//            book.Borrowers.Remove(user.FirstName);
//            user.BorrowedBooks.Remove(book.Name);
//            SaveData();
//            Console.WriteLine($"{book.Name} qaytarildi!");
//        }
//        else
//        {
//            Console.WriteLine("Bu kitob sizda mavjud emas yoki noto‘g‘ri nom kiritildi!");
//        }
//    }

//    private void SaveData()
//    {
//        File.WriteAllText(BooksFile, JsonConvert.SerializeObject(books, Newtonsoft.Json.Formatting.Indented));
//        File.WriteAllText(UsersFile, JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented));
//    }
//}
