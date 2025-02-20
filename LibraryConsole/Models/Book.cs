﻿using System;
namespace LibraryConsole.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public List<string> Readers { get; set; } = new List<string>();

        public Book(int id, string title, string genre, string author, int totalQuantity)
        {
            Id = id;
            Title = title;
            Genre = genre;
            Author = author;
            TotalQuantity = totalQuantity;
            AvailableQuantity = totalQuantity;
        }
    }

}
