using IWantApp.Domain.Users;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserCreator userCreator)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.name),
            new Claim("CreatedBy", userId)
        };

        (IdentityResult identity, string newUserId) = await userCreator.Create(employeeRequest.Email, employeeRequest.password, userClaims);

        if (!identity.Succeeded)
            return Results.ValidationProblem(identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/employees/{newUserId}", newUserId);
    }
}
