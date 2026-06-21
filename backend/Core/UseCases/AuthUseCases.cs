using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class AuthUseCases : IAuthUseCases
{
    private readonly IAuthGateway _authGateway;

    public AuthUseCases(IAuthGateway authGateway)
    {
        if (authGateway is null)
        {
            throw new Exception("AuthGateway est obligatoire.");
        }
        _authGateway = authGateway;
    }

    public LoginResponse? Login(AuthenticationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(request.MotDePasse))
        {
            return null;
        }
        return _authGateway.Login(request);
    }
}