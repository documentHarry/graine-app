using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class RoleRoutes
{
    public static WebApplication AddRoleRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/roles")
            .WithTags("Roles")
            .RequireAuthorization(policy => policy.RequireRole("ADMIN"));

        group.MapGet("", (IRoleUseCases roleUseCases) =>
        {
            var roles = roleUseCases.GetAllRoles();
            return Results.Ok(roles);
        })
        .WithName("GetAllRoles")
        .Produces<IEnumerable<Role>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (Role role, IRoleUseCases roleUseCases) =>
        {
            roleUseCases.AddRole(role);
            return Results.Created();
        })
        .WithName("AddRole")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IRoleUseCases roleUseCases) =>
        {
            var role = roleUseCases.GetRoleById(id);
            if (role is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(role);
        });

        group.MapPut("{id:int}", (int id, Role role, IRoleUseCases roleUseCases) =>
        {
            role.IdRole = id;
            roleUseCases.UpdateRole(role);
            return Results.NoContent();
        });

        group.MapDelete("{id:int}", (int id, IRoleUseCases roleUseCases) =>
        {
            roleUseCases.DeleteRole(id);
            return Results.NoContent();
        });

        return app;
    }
}