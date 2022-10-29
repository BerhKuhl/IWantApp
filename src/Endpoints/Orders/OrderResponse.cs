namespace IWantApp.Endpoints.Orders;

public record OrderResponse(string ClientId, string ClientEmail, IEnumerable<OrderProduct> Products, string DeliveryAdress, decimal Total, bool IsValid,
    string CreatedBy, DateTime CreatedOn, string EditedBy, DateTime EditedOn);

public record OrderProduct(Guid Id, string Name);
