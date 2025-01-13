using System;
using System.Collections.Generic;

public abstract class LibraryItem
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public bool Available { get; set; }

    public LibraryItem(string title, string author, DateTime publicationDate)
    {
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
        Available = true;
    }

    public abstract void Checkout();
    public abstract void ReturnItem();
}

public class Book : LibraryItem
{
    public string Genre { get; set; }

    public Book(string title, string author, DateTime publicationDate, string genre)
        : base(title, author, publicationDate)
    {
        Genre = genre;
    }

    public override void Checkout()
    {
        if (Available)
        {
            Available = false;
            Console.WriteLine($"{Title} has been checked out.");
        }
        else
        {
            Console.WriteLine($"{Title} is not available.");
        }
    }

    public override void ReturnItem()
    {
        Available = true;
        Console.WriteLine($"{Title} has been returned.");
    }
}

public class DVD : LibraryItem
{
    public int Runtime { get; set; }

    public DVD(string title, string author, DateTime publicationDate, int runtime)
        : base(title, author, publicationDate)
    {
        Runtime = runtime;
    }

    public override void Checkout()
    {
        if (Available)
        {
            Available = false;
            Console.WriteLine($"{Title} has been checked out.");
        }
        else
        {
            Console.WriteLine($"{Title} is not available.");
        }
    }

    public override void ReturnItem()
    {
        Available = true;
        Console.WriteLine($"{Title} has been returned.");
    }
}

public class LibraryCatalog
{
    private List<LibraryItem> items;

    public LibraryCatalog()
    {
        items = new List<LibraryItem>();
    }

    public void AddItem(LibraryItem item)
    {
        items.Add(item);
    }

    // Cập nhật phương thức FindItem để trả về một danh sách các item tìm được
    public List<LibraryItem> FindItem(string? searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine("Search term cannot be empty.");
            return new List<LibraryItem>();
        }

        return items.FindAll(item => item.Title.Contains(searchTerm) || item.Author.Contains(searchTerm));
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Tạo đối tượng LibraryCatalog
        LibraryCatalog catalog = new LibraryCatalog();

        // Thêm Book và DVD vào catalog
        catalog.AddItem(new Book("The Great Gatsby", "F. Scott Fitzgerald", new DateTime(1925, 4, 10), "Novel"));
        catalog.AddItem(new DVD("The Matrix", "Lana Wachowski", new DateTime(1999, 3, 31), 136));

        // Tìm kiếm sách theo tên hoặc tác giả
        Console.WriteLine("Search for a book or DVD:");
        string searchTerm = Console.ReadLine();
        var foundItems = catalog.FindItem(searchTerm); // Tìm kiếm danh sách các item

        if (foundItems.Count > 0)
        {
            foreach (var item in foundItems)
            {
                Console.WriteLine($"Found: {item.Title} by {item.Author}. Available: {item.Available}");
            }
        }
        else
        {
            Console.WriteLine("No items found.");
        }

        // Kiểm tra và trả lại sách/DVD
        Console.WriteLine("Checkout an item (enter title):");
        string checkoutTitle = Console.ReadLine();
        var checkoutItems = catalog.FindItem(checkoutTitle);  // Tìm kiếm item

        if (checkoutItems.Count > 0)
        {
            checkoutItems[0].Checkout(); // Gọi phương thức Checkout cho item đầu tiên
        }

        Console.WriteLine("Return an item (enter title):");
        string returnTitle = Console.ReadLine();
        var returnItems = catalog.FindItem(returnTitle);  // Tìm kiếm item

        if (returnItems.Count > 0)
        {
            returnItems[0].ReturnItem(); // Gọi phương thức ReturnItem cho item đầu tiên
        }
    }
}
