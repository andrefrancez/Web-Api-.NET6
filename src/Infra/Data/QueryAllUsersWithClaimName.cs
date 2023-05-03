using Dapper;
using System.Data.SqlClient;
using WebApiUdemy.Endpoints.Employees;

namespace WebApiUdemy.Infra.Data;

public class QueryAllUsersWithClaimName {
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration) {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows) {
        var db = new SqlConnection(configuration["ConnectionString:WebApiTestDb"]);
        var query = @"SELECT Email, ClaimValue AS Name FROM AspNetUsers u 
                INNER JOIN AspNetUserClaims c
                ON u.Id = c.UserId and claimtype = 'Name' ORDER BY name
                OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
    }
}
