using Application.Models.Requests;
using Models.Entities;


namespace Application.Abstractions.Services
{
    public interface ITokenService
{
    string CreateToken(CreateTokenRequest request);
}
}
