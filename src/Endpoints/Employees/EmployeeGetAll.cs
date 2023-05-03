using Microsoft.AspNetCore.Authorization;
using WebApiUdemy.Infra.Data;

namespace WebApiUdemy.Endpoints.Employees;

public class EmployeeGetAll {
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query) {
        if (page == null)
            page = 1;

        if (rows == null || rows < 1 || rows > 10)
            rows = 10;

        var result = await query.Execute(page.Value, rows.Value);

        return Results.Ok(result);
    }
}
