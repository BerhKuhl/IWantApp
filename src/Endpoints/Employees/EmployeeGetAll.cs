namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(QueryAllUsersWithClaimName query, int page = 1, int rows = 10)
    {
        var result = await query.Execute(page, rows);
        return Results.Ok(result);
    }
}
