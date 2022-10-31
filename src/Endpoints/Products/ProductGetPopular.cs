namespace IWantApp.Endpoints.Products;

public class ProductGetPopular
{
    public static string Template => "/products/popular";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(QueryPopularProducts query, int page = 1, int rows = 10)
    {
        if (rows > 10) return Results.BadRequest("Max 10 rows");

        var result = await query.Execute(page, rows);

        return Results.Ok(result);
    }
}
