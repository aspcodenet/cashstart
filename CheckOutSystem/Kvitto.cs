using Che;

namespace CheckOutSystem;

public class Kvitto
{
    public Kvitto()
    {
        Datum = DateTime.Now;
    }

    public DateTime Datum { get; set; }

    public List<ReceiptRow> Rows { get; set; } = new();

    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (var row in Rows)
            total += row.GetTotal();
        return total;
    }

    public void AddToReceipt(Product product, int antal)
    {
        var existing = Rows.FirstOrDefault(x => x.ProductId == product.ProductId);
        if (existing == null)
        {
            existing = new ReceiptRow
            {
                PerPrice = product.Price,
                ProductId = product.ProductId,
                ProductName = product.Name
            };
            Rows.Add(existing);
        }

        existing.Quantity += antal;
    }
}

public class ReceiptRow
{
    public decimal PerPrice { get; set; }

    public decimal Quantity { get; set; }
    public string ProductName { get; set; }
    public string ProductId { get; set; }

    public decimal GetTotal()
    {
        return PerPrice * Quantity;
    }
}