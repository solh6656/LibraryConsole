using System;
using System.Collections.Generic;
using System.IO;
using LibraryConsole.Models;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

class Program
{
    static List<Book> books = new List<Book>();
    static List<Reader> readers = new List<Reader>();
    static Reader currentReader;

    static void Main()
    {
        LoadData();

        while (true)
        {
            Console.WriteLine("Login - 1 yoki Register? - 2 Tugatish - 0");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Login();
            }
            else if (choice == 2)
            {
                Register();
            }
            else if (choice == 0)
            {
                break;
            }
            else
            {
                Console.Clear();

                Console.WriteLine("Noto'g'ri tanlov.");
            }

            if (currentReader != null)
            {
                ShowMenu();
            }
        }
    }

    static void LoadData()
    {
        if (File.Exists("books.json"))
        {
            books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText("../../../Data/books.json"));
        }
        if (File.Exists("readers.json"))
        {
            readers = JsonConvert.DeserializeObject<List<Reader>>(File.ReadAllText("../../../Data/readers.json"));
        }
    }

    static void SaveData()
    {
        File.WriteAllText("../../../Data/books.json", JsonConvert.SerializeObject(books, Formatting.Indented));
        File.WriteAllText("../../../Data/readers.json", JsonConvert.SerializeObject(readers, Formatting.Indented));
    }

    static void Login()
    {
        Console.Write("Login: ");
        string login = Console.ReadLine();
        Console.Write("Parol: ");
        string password = Console.ReadLine();

        Console.Clear();
        currentReader = readers.Find(r => r.Login == login && r.Password == password);
        if (currentReader == null)
        {
            Console.Clear();
            Console.WriteLine("Login yoki parol noto'g'ri.");
        }
    }

    static void Register()
    {
        Console.Write("Login: ");
        string login = Console.ReadLine();
        Console.Write("Parol: ");
        string password = Console.ReadLine();
        Console.Write("Ism: ");
        string firstName = Console.ReadLine();
        Console.Write("Familiya: ");
        string lastName = Console.ReadLine();
        Console.Write("Yosh: ");
        int age = int.Parse(Console.ReadLine());

        Reader newReader = new Reader
        {
            Login = login,
            Password = password,
            Id = readers.Count + 1,
            FirstName = firstName,
            LastName = lastName,
            Age = age
        };

        readers.Add(newReader);
        SaveData();
        Console.WriteLine("Ro'yxatdan o'tish muvaffaqiyatli.");
    }

    static void ShowMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Kitoblar ro'yxatini ko'rish");
            Console.WriteLine("2. Kitob haqida ma'lumot olish");
            Console.WriteLine("3. Kitob olish");
            Console.WriteLine("4. Kitob qaytarish");
            Console.WriteLine("5. Chiqish");
            Console.Write("Tanlovingizni kiriting: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewBooks();
                    break;
                case "2":
                    ViewBookDetails();
                    break;
                case "3":
                    BorrowBook();
                    break;
                case "4":
                    ReturnBook();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Noto'g'ri tanlov.");
                    break;
            }
        }
    }

    static void ViewBooks()
    {
        Console.Clear();
        Console.WriteLine("\nKitoblar ro'yxati:");
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id}. {book.Title} - {book.Author} ({book.AvailableQuantity}/{book.TotalQuantity})");
        }
    }

    static void ViewBookDetails()
    {
        ViewBooks();
        Console.Write("Ma'lumot olmoqchi bo'lgan kitob raqamini kiriting: ");
        int bookId = int.Parse(Console.ReadLine());
        Book book = books.Find(b => b.Id == bookId);
        if (book != null)
        {
            Console.Clear();
            Console.WriteLine($"Nom: {book.Title}");
            Console.WriteLine($"Janr: {book.Genre}");
            Console.WriteLine($"Avtor: {book.Author}");
            Console.WriteLine($"Jami miqdor: {book.TotalQuantity}");
            Console.WriteLine($"Kutubxonada qolgan miqdor: {book.AvailableQuantity}");
            Console.WriteLine("Kitobni o'qiyotganlar: " + string.Join(", ", book.Readers));
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kitob topilmadi.");
        }
    }

    static void BorrowBook()
    {
        ViewBooks();
        Console.Write("Olish uchun kitob raqamini kiriting: ");
        int bookId = int.Parse(Console.ReadLine());
        Book book = books.Find(b => b.Id == bookId);
        if (book != null && book.AvailableQuantity > 0)
        {
            book.AvailableQuantity--;
            book.Readers.Add(currentReader.FirstName);
            currentReader.BorrowedBooks.Add(book);
            currentReader.BorrowedBooksCount++;
            SaveData();
            Console.Clear();
            Console.WriteLine("Kitob olindi.");
        }
        else
        {
            Console.WriteLine("Kitob mavjud emas yoki tugagan.");
        }
    }

    static void ReturnBook()
    {
        if (currentReader.BorrowedBooks.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Sizda hech qanday kitob yo'q.");
            return;
        }

        Console.Clear();
        Console.WriteLine("\nOlgan kitoblaringiz:");
        for (int i = 0; i < currentReader.BorrowedBooks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {currentReader.BorrowedBooks[i].Title}");
        }
        Console.Write("Qaytarish uchun kitob raqamini kiriting: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < currentReader.BorrowedBooks.Count)
        {
            Book book = currentReader.BorrowedBooks[index];
            book.AvailableQuantity++;
            book.Readers.Remove(currentReader.FirstName);
            currentReader.BorrowedBooks.RemoveAt(index);
            currentReader.BorrowedBooksCount--;
            SaveData();
            Console.Clear();
            Console.WriteLine("Kitob qaytarildi.");
        }
        else
        {
            Console.WriteLine("Noto'g'ri raqam.");
        }
    }
}
