using IWantApp.Domain.Users;

namespace IWantApp.Endpoints.Clients;

public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest clientRequest, UserCreator userCreator)
    {
        var userClaims = new List<Claim>
        {
            new Claim("Name", clientRequest.Name),
            new Claim("Cpf", clientRequest.Cpf)
        };

        (IdentityResult identity, string userId) = await userCreator.Create(clientRequest.Email, clientRequest.Password, userClaims);

        if (!identity.Succeeded)
            return Results.ValidationProblem(identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/clients/{userId}", userId);
    }
}
