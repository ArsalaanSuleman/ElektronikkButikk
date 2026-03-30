public class Customer
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PhoneNumber {get; set;} = string.Empty;

    public List<Order> Orders {get; set;} = new();
}

/*
    Hvorfor List<Order> Orders?
    Dette kalles en en-til-mange relasjon — 
    én kunde kan ha mange bestillinger. 
    EF ser denne propertyen og lager automatisk en fremmednøkkel i 
    Orders-tabellen som peker tilbake til Customers.
*/