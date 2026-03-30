
public class InMemoryServiceBus : IServiceBus
{
    public void Publish<T>(T message)
    {
        // I en ekte service bus ville meldingen blitt sendt
        // til en kø i skyen (f.eks. Azure Service Bus).
        // Her printer vi bare for å simulere det.

        Console.WriteLine($"[ServiceBus] Melding sendt: {typeof(T).Name}");
        Console.WriteLine($"[ServiceBus] Innhold: {System.Text.Json.JsonSerializer.Serialize(message)}");

    }
}