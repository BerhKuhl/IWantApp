namespace IWantApp.Endpoints.Products;

public record ProductsResponse(Guid Id, string Name, string CategoryName, string Description, bool HasStock, decimal Price, bool Active);

public record ProductsPopularResponse(Guid Id, string Name, string CategoryName, decimal Price, bool HasStock, int Quantity);