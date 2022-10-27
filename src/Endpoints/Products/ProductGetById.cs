using IWantApp.Domain.Products;

namespace IWantApp.Endpoints.Products;

public class ProductGetById
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var product = await context.Products.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefaultAsync();

        if (product == null) return Results.NotFound();

        var result = new ProductsResponse(product.Id, product.Name, product.Category.Name, product.Description, product.HasStock, product.Price, product.Active);

        return Results.Ok(result);
    }
}
