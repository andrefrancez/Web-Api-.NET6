using WebApiUdemy.Infra.Data;

namespace WebApiUdemy.Endpoints.Employees;

public class EmployeeGetAll {
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query) {
        if (page == null)
            page = 1;

        if (rows == null || rows < 1 || rows > 10)
            rows = 10;

        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}
