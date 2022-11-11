using Che;

namespace CheckOutSystem;

public class App
{
    public void Run()
    {
        var products = ReadProductsFromFile();

        while (true)
        {
            Console.WriteLine("1. Ny kund:");
            Console.WriteLine("2. Avsluta:");
            Console.Write("Val:");
            var val = Console.ReadLine();
            if (val == "2")
                break;
            if (val == "1")
            {
                Console.Clear();
                NewReceipt(products);
            }
        }
    }

    private void WriteReceipt(Kvitto kvitto)
    {
        Console.WriteLine("Kvitto på Stefans Affär");
        Console.WriteLine(kvitto.Datum.ToString("YYYY-mm-dd"));
        foreach (var row in kvitto.Rows)
            Console.WriteLine($"{row.ProductName} {row.Quantity} * {row.PerPrice} = {row.GetTotal()}");
        Console.WriteLine($"TOTAL: {kvitto.GetTotal()}");
    }

    private void NewReceipt(List<Product> allGoods)
    {
        var kvitto = new Kvitto();

        while (true)
        {
            WriteReceipt(kvitto);
            Console.WriteLine("<Produkt ID> <ANTAL>");
            var id = Console.ReadLine();
            if (id == "PAY") break;

            if (id.Length == 0)
            {
                Console.WriteLine("Wrong product ID or amount!");
                continue;
            }

            var answer = id.Split(' ');

            var product = allGoods.FirstOrDefault(e => e.ProductId == answer[0]);
            if (product == null)
            {
                Console.WriteLine("Invalid Product ID");
                continue;
            }

            kvitto.AddToReceipt(product, Convert.ToInt32(answer[1]));
        }
    }




    private List<Product> ReadProductsFromFile()
    {
        var result = new List<Product>();

        foreach (var line in File.ReadLines("Products.txt"))
        {
            var parts = line.Split(';');
            var product = new Product();
            product.Name = parts[1];
            product.ProductId = parts[0];
            product.Price = Convert.ToInt32(parts[2]);
            product.antal = 0;
            result.Add(product);
        }

        return result;
    }
}