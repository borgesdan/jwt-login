using Marketplace.Application.Contracts;
using Marketplace.Application.Data.Context;
using Marketplace.Application.Data.Entities;
using Marketplace.Application.Data.Shared;
using Marketplace.Application.Errors;
using Marketplace.Application.Helpers;
using Marketplace.Application.Services.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Marketplace.Application.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IResultData> Create(UserPostRequest request, AccessLevelType accessLevel)
        {
            var validation = new UserCreateValidator(request, accessLevel);

            if (!validation.Validate())
                return ResultData.Error(validation.Errors.First());

            var result = await CreateTransaction(request);

            if (!result.Succeeded)
                return result;

            return ResultData.Ok();
        }

        private async Task<IResultData> CreateTransaction(UserPostRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var user = new UserEntity
                {
                    AccessLevel = AccessLevelType.Buyer,
                    CreationDate = DateTime.UtcNow,
                    PrimaryLogin = request.Email,
                    Password = SecurityHelper.CreateSHA1Hash(request.Password),
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var person = new PersonEntity
                {
                    Name = request.UserName,
                    UserId = user.Id,
                };

                _context.Persons.Add(person);
                await _context.SaveChangesAsync();

                transaction.Commit();

                return ResultData.Ok();
            }
            catch
            {
                transaction.Rollback();
            }

            return ResultData.InternalError(InternalError.UserCreateTransaction.Value);
        }

        public async Task<IResultData> Login(UserLoginRequest request)
        {
            var validator = new UserLoginValidator(request);

            if (!validator.Validate())
                return ResultData.Error(validator.Errors.First());

            var passwordHash = SecurityHelper.CreateSHA1Hash(request.Password);

            var user = await _context.Users.Where(u =>
                    (u.PrimaryLogin == request.Email || u.SecondaryLogin == request.Email)
                    && u.Password == passwordHash)
                .FirstOrDefaultAsync();

            if (user == null)
                return ResultData.Error(AppError.User.InvalidLogin.Message);

            var tokenRequest = new UserTokenRequest
            {
                UserUid = user.Uid,
                AccessLevel = user.AccessLevel,
                Email = request.Email
            };

            var tokenResult = GenerateToken(tokenRequest);
            var tokenResponse = tokenResult.GetData<UserTokenResponse>();

            return ResultData.Ok(tokenResponse);
        }

        public IResultData GenerateToken(UserTokenRequest request)
        {
            var jwtKeyValue = _configuration["Jwt:Key"];
            var tokenExpireValue = _configuration["TokenConfiguration:ExpireHours"];
            var tokenIssuerValue = _configuration["TokenConfiguration:Issuer"];
            var tokenAudienceValue = _configuration["TokenConfiguration:Audience"];

            if(jwtKeyValue == null || tokenExpireValue == null
                || tokenIssuerValue == null || tokenAudienceValue == null)
            {
                return ResultData.InternalError(InternalError.ConfigurationAccessJwtAccess.Value);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKeyValue));
            var expiration = DateTime.UtcNow.AddHours(double.Parse(tokenExpireValue));
            var claims = new[]
            {                
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, ((int)request.AccessLevel).ToString()),
                new Claim(ClaimTypes.NameIdentifier, request.UserUid.ToString()),                
            };                        

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = tokenAudienceValue,
                Issuer = tokenIssuerValue,
                Expires = expiration,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new UserTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,                
            };

            return ResultData.Ok(response);
        }
    }
}