using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApiUdemy.Endpoints.Employees;

public class EmployeePost {
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManage) {
        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = userManage.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userClaims = new List<Claim> {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name)
        };

        var claimResult = userManage.AddClaimsAsync(user, userClaims).Result;

        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());
        
        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}
