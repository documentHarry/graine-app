using Core.IGateways;
using Core.UseCases;
using Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<ICategorieRepository, CategorieRepository>();
        services.AddTransient<ICategorieGateway, CategorieGateway>();

        services.AddTransient<IAromateRepository, AromateRepository>();
        services.AddTransient<IAromateGateway, AromateGateway>();

        services.AddTransient<IEspeceRepository, EspeceRepository>();
        services.AddTransient<IEspeceGateway, EspeceGateway>();

        services.AddTransient<IProprieteMedicinaleRepository, ProprieteMedicinaleRepository>();
        services.AddTransient<IProprieteMedicinaleGateway, ProprieteMedicinaleGateway>();

        services.AddTransient<IVarieteRepository, VarieteRepository>();
        services.AddTransient<IVarieteGateway, VarieteGateway>();

        services.AddTransient<IProduitRepository, ProduitRepository>();
        services.AddTransient<IProduitGateway, ProduitGateway>();

        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IRoleGateway, RoleGateway>();

        services.AddTransient<IAdresseLivraisonRepository, AdresseLivraisonRepository>();
        services.AddTransient<IAdresseLivraisonGateway, AdresseLivraisonGateway>();

        services.AddTransient<IUtilisateurRepository, UtilisateurRepository>();
        services.AddTransient<IUtilisateurGateway, UtilisateurGateway>();

        services.AddTransient<ILocaliteRepository, LocaliteRepository>();
        services.AddTransient<ILocaliteGateway, LocaliteGateway>();

        services.AddTransient<IUtilisateurRoleRepository, UtilisateurRoleRepository>();
        services.AddTransient<IUtilisateurRoleGateway, UtilisateurRoleGateway>();

        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IAuthGateway, AuthGateway>();

        return services;
    }
}