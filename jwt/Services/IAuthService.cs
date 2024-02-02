using jwt.Models;

namespace jwt.Services
{
    public interface IAuthService
    {
        Task<AuthModel> registerAsync(RegisterModel registerModel);


    }
}
