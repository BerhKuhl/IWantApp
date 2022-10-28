namespace IWantApp.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(Guid? orderId, Guid? clientId, HttpContext http, ApplicationDbContext context, int page = 1, int row = 10, string orderBy = "CreatedOn")
    {
        var clientClaim = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var claim = http.User.Claims.FirstOrDefault(c => c.Type == "Cpf" || c.Type == "EmployeeCode");

        if (claim == null) return Results.Unauthorized();

        switch (claim.Type)
        {
            case "Cpf":
                if (orderId == null) return Results.BadRequest("Order Id is required!");

                var order = await context.Orders.AsNoTracking().Include(o => o.Products).Where(o => o.Id == orderId).FirstOrDefaultAsync();
                if (order == null) return Results.NotFound("Order not found");
                if (order.ClientId != clientClaim) return Results.Unauthorized();

                var products = order.Products.Select(p => new OrderProduct(p.Id, p.Name));

                //var result = new OrderResponse(order.ClientId, products, order.DeliveryAdress, order.Total, order.IsValid,
                //    order.CreatedBy, order.CreatedOn, order.EditedBy, order.EditedOn);

                return Results.Ok();
            case "EmployeeCode":
                //if (row > 10) return Results.BadRequest("Row with max 10");

                //var queryBase = context.Orders.AsNoTracking().Include(o => o.Products).Where(o => o.ClientId == clientId.ToString());

                ////if (clientId != null) queryBase = queryBase.Where(o => o.ClientId == clientId.ToString());

                //if (orderBy == "CreatedOn")
                //    queryBase = queryBase.OrderBy(o => o.CreatedOn);
                //else if (orderBy == "Total")
                //    queryBase = queryBase.OrderBy(o => o.Total);
                //else
                //    return Results.BadRequest("Order only by CreatedOn or Total");

                //var queryFilter = queryBase.Skip((page - 1) * row).Take(row);

                //var orders = queryFilter.ToList();

                ////var product = orders.Select(p => new OrderProduct(p.Products., p.Name));

                //var results = orders.Select(o => new OrderResponse(o.ClientId, new , o.DeliveryAdress, o.Total, o.IsValid,
                //    o.CreatedBy, o.CreatedOn, o.EditedBy, o.EditedOn));

                //return Results.Ok(results);
            default:
                return Results.Unauthorized();
        }
    }
}
