namespace IWantApp.Endpoints.Orders;

public record OrderResponse(string ClientId, IEnumerable<Product> Products, string DeliveryAdress, decimal Total, bool IsValid,
    string CreatedBy, DateTime CreatedOn, string EditedBy, DateTime EditedOn);

public record OrderProduct(Guid Id, string Name);
