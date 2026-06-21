using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IAuthUseCases
{
    LoginResponse? Login(AuthenticationRequest request);
}