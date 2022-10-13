using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace src.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration _configuration;
    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(_configuration["ConnectionString:IWantDb"]);

        var query = @"SELECT Email, ClaimValue as Name
                FROM AspNetUsers u
                INNER JOIN AspNetUserClaims c
                ON u.Id = c.UserId and claimType = 'Name'
                ORDER BY name
                OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return db.Query<EmployeeResponse>(query, new { page, rows });
    }
}
