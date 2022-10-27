namespace IWantApp.Endpoints.Products;

public record ProductsResponse(string Name, string CategoryName, string Description, bool HasStock, decimal Price, bool Active);
