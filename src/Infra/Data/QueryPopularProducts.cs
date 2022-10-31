namespace IWantApp.Infra.Data;

public class QueryPopularProducts
{
    private readonly IConfiguration _configuration;
    public QueryPopularProducts(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<ProductsPopularResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(_configuration["ConnectionString:IWantDb"]);

        var query =
            @"SELECT p.Id, p.Name, c.Name CategoryName, p.Price, p.HasStock, COUNT(*) Quantity
            FROM OrderProducts op
            INNER JOIN Products p
            ON p.Id = op.ProductsId
            INNER JOIN Categories c
            ON c.Id = p.CategoryId
            GROUP BY op.ProductsId, p.Id, p.Name, c.Name, p.Price, p.HasStock
            HAVING COUNT(*) > 1
            ORDER BY Quantity DESC
            OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return await db.QueryAsync<ProductsPopularResponse>(query, new { page, rows });
    }
}
