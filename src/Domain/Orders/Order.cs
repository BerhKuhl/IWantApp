namespace IWantApp.Domain.Orders;

public class Order : Entity
{
    public string ClientId { get; private set; }
    public List<Product> Products { get; private set; }
    public decimal Total { get; private set; }
    public string DeliveryAdress { get; private set; }

    private Order() { }

    public Order(string clientId, string clientName, List<Product> products, string deliveryAdress)
    {
        ClientId = clientId;
        Products = products;
        DeliveryAdress = deliveryAdress;
        CreatedBy = clientName;
        EditedBy = clientName;
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;

        Total = 0;
        foreach (var item in Products) Total += item.Price;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNullOrEmpty(ClientId, "Client")
            .IsNotNullOrEmpty(DeliveryAdress, "Delivery Address")
            .IsTrue(Products != null || Products.Any(), "Products");
        AddNotifications(contract);
    }
}
