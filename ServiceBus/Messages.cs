public class OrderCreatedMessage
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    // Sendes når en bestilling er opprettet
    // Andre systemer (f.eks. lagersystem, e-post) kan lytte på denne
}

public class LowStockMessage
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int RemainingStock { get; set; }
    // Sendes når lagerbeholdningen blir lav
}