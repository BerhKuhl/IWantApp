namespace IWantApp.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(Guid? orderId, HttpContext http, ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        // Buscando claims no Token
        var clientClaim = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var claim = http.User.Claims.FirstOrDefault(c => c.Type == "Cpf" || c.Type == "EmployeeCode");

        // Validando se os parâmetros foram passados corretamente
        if (claim == null) return Results.Unauthorized();
        if (orderId == null) return Results.BadRequest("Order Id is required!");

        // Buscando e validando o Pedido
        var order = await context.Orders.AsNoTracking().Include(o => o.Products).Where(o => o.Id == orderId).FirstOrDefaultAsync();
        if (order == null) return Results.NotFound("Order not found");
        if (order.ClientId != clientClaim && claim.Type != "EmployeeCode") return Results.Unauthorized();

        // Tranformando os Produtos na classe de resposta
        var products = order.Products.Select(p => new OrderProduct(p.Id, p.Name));

        // Buscando o usuário do pedido
        var clientOrder = await userManager.FindByIdAsync(order.ClientId);

        // Transformando todas as buscas na classe de resposta.
        var result = new OrderResponse(order.ClientId, clientOrder.Email, products, order.DeliveryAdress, order.Total, order.IsValid,
            order.CreatedBy, order.CreatedOn, order.EditedBy, order.EditedOn);

        return Results.Ok(result);
    }
}
