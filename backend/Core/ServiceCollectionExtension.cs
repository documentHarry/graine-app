using Core.UseCases.Abstractions;
using Core.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<ICategorieUseCases, CategorieUseCases>();
        services.AddTransient<IAromateUseCases, AromateUseCases>();
        services.AddTransient<IEspeceUseCases, EspeceUseCases>();
        services.AddTransient<IProprieteMedicinaleUseCases, ProprieteMedicinaleUseCases>();
        services.AddTransient<IVarieteUseCases, VarieteUseCases>();
        services.AddTransient<IProduitUseCases, ProduitUseCases>();
        services.AddTransient<IRoleUseCases, RoleUseCases>();
        services.AddTransient<IAdresseLivraisonUseCases, AdresseLivraisonUseCases>();
        services.AddTransient<IUtilisateurUseCases, UtilisateurUseCases>();
        services.AddTransient<ILocaliteUseCases, LocaliteUseCases>();
        services.AddTransient<IUtilisateurRoleUseCases, UtilisateurRoleUseCases>();
        services.AddTransient<IAuthUseCases, AuthUseCases>();

        return services;
    }
}