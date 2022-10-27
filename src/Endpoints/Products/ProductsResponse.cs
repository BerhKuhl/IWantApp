namespace IWantApp.Endpoints.Products;

public record ProductsResponse(string Name, string CategoryName, string Description, bool HasStock, bool Active);
