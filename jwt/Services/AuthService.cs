using jwt.Helpers;
using jwt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly Jwt _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<Jwt> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel> registerAsync(RegisterModel registerModel)
        {
            if (await _userManager.FindByEmailAsync(registerModel.Email) is not null)
            {
                return new AuthModel()
                { Message = "Email is Already Registered" };

            }
            if (await _userManager.FindByNameAsync(registerModel.UserName) is not null)
            {
                return new AuthModel()
                { Message = "User Name is Already Registered" };
            }
            var user = new ApplicationUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                FirstName = registerModel.FName,
                LastName = registerModel.FName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description} ,";


                }

                return new AuthModel() { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await creatjwttoken(user);

            return new AuthModel()
            {
                Email = user.Email,
                Expireson = jwtSecurityToken.ValidTo,
                IsAuthentecated = true,
                Roles = new List<string> { "User" },
                token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName


            };


        }


        private async Task<JwtSecurityToken> creatjwttoken(ApplicationUser user)
        {
            var userclaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var RolesClaims = new List<Claim>();
            foreach (var role in roles)
            {
                RolesClaims.Add(new Claim("roles", role));

            }
            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim ("uid", user.Id)
            }.Union(userclaims)
             .Union(RolesClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.issuer,
                audience: _jwt.auddience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.duretionindayes),
                signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }

    }
}
