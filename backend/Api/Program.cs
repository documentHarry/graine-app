using Api.EndPoints;
using Core;
using Infrastructure;
using Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCoreServices();

var databaseProvider = builder.Configuration["DatabaseProvider"];

switch (databaseProvider)
{
    case "SqlServer":
        builder.Services.AddSingleton<IDbConnectionFactory, SqlServerConnectionFactory>();
        break;
    case "MySql":
        builder.Services.AddSingleton<IDbConnectionFactory, MySqlConnectionFactory>();
        break;
    default:
        throw new InvalidOperationException($"DatabaseProvider inconnu : {databaseProvider}"
    );
}

builder.Services.AddInfrastructureServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("AllowLocalhost");
}

app.UseHttpsRedirection();

app.AddCategorieRoutes();
app.AddAromateRoutes();
app.AddEspeceRoutes();
app.AddProprieteMedicinaleRoutes();
app.AddVarieteRoutes();
app.AddProduitRoutes();
app.AddRoleRoutes();
app.AddAdresseLivraisonRoutes();
app.AddUtilisateurRoutes();
app.AddLocaliteRoutes();
app.AddUtilisateurRoleRoutes();
app.AddAuthRoutes();

app.MapGet("/", () => "API Graines fonctionne.");

app.Run();


















