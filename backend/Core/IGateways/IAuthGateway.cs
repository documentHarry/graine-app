using Core.Models;

namespace Core.IGateways;

public interface IAuthGateway
{
    LoginResponse? Login(AuthenticationRequest request);
}