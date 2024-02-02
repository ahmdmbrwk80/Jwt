using jwt.Models;

namespace jwt.Services
{
    public interface IAuthService
    {
        Task<AuthModel> registerAsync(RegisterModel registerModel);
        Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel);
        Task<string> AddRoleAsync(AddRoleModel addRoleModel);


    }
}
