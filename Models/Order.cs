

public class Order
{
    public int Id {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public int CustomerId {get; set;}
    public Customer? Customer {get; set;}
    public List<OrderItem> Items {get; set;} = new();
}

public class OrderItem
{
    public int Id {get; set;}
    public int OrderId {get; set;}
    public int ProductId {get; set;}
    public Product Product {get; set;} = null!;
    public int Quantity {get; set;}
    public decimal PriceAtPurchase { get; set; }
}

/*
    PriceAtPurchase?

    Dette er viktig forretningslogikk — hvis du handler en laptop til 9999kr i dag, 
    og prisen endres til 12999kr neste uke, skal bestillingen din fortsatt vise 9999kr.
*/