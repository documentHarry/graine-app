using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class UtilisateurRoleRoutes
{
    public record UpdateUtilisateurRolesRequest(int UtilisateurId, List<int> RolesIds);

    public static WebApplication AddUtilisateurRoleRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/utilisateurs-roles")
            .WithTags("UtilisateursRoles")
            .RequireAuthorization(policy => policy.RequireRole("ADMIN"));

        group.MapGet("{utilisateurId:int}", (int utilisateurId, IUtilisateurRoleUseCases useCases) =>
        {
            var roles = useCases.GetUtilisateurRoles(utilisateurId);
            return Results.Ok(roles);
        });

        group.MapPut("{utilisateurId:int}", (int utilisateurId,
            UpdateUtilisateurRolesRequest request, IUtilisateurRoleUseCases useCases) =>
        {
            useCases.UpdateUtilisateurRoles(utilisateurId, request.RolesIds);
            var roles = useCases.GetUtilisateurRoles(utilisateurId);
            return Results.Ok(roles);
        });

        return app;
    }
}