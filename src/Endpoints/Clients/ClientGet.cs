﻿using IWantApp.Domain.Users;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Clients;

public class ClientGet
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(HttpContext http)
    {
        var userId = http.User;
        var result = new
        {
            Id = userId.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            Name = userId.Claims.First(c => c.Type == "Name").Value,
            Cpf = userId.Claims.First(c => c.Type == "Cpf").Value
        };
        

        return Results.Ok(result);
    }
}
