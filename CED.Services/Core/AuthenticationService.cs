using CED.Data;
using CED.Models;
using CED.Models.Core;
using CED.Models.DTO;
using CED.Models;
using CED.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Data;

namespace CED.Services.Core
{
    public class AuthenticationService : DataProvider, IAuthenticationService
    {

        private readonly IUserService _userService;
        private readonly JwtToken _jwtToken;
        private readonly ILogger<AuthenticationService> _log;

        public AuthenticationService(
            ILogger<AuthenticationService> log,
            IOptions<JwtToken> jwtToken,
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
            _log = log;
            _jwtToken = jwtToken.Value;
        }
        
        public async Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto)
        {
            var user = await GetUserByEmail(loginRequestDto.Email);
            var authenticationDTO = new AuthenticationDTO();
            if (user == null)
            {
                return UserNotFound(loginRequestDto);
            }

            if (Hash.GetHash(loginRequestDto.Password, user.PasswordSalt).Hash != user.Password)
            {
                return UserPasswordIncorrect(loginRequestDto);
            }


            authenticationDTO.IsAuthenticated = true;
            authenticationDTO.Token = await CreateJwtToken(user);
            authenticationDTO.UserId = user.Id;

            if (user.RefreshTokens != null && user.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive);
                authenticationDTO.RefreshToken = activeRefreshToken.Token;
                authenticationDTO.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = await CreateRefreshToken();
                authenticationDTO.RefreshToken = refreshToken.Token;
                authenticationDTO.RefreshTokenExpiration = refreshToken.Expires;
                user.RefreshTokens ??= new List<RefreshToken>();
                user.RefreshTokens.Add(refreshToken);
                await SaveRefreshToken(refreshToken, user.Id);
            }

            return authenticationDTO;
        }

        public Task Logout(string token)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationDTO> RefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateJwtToken(User user)
        {
            return await Task.Run(() =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtToken.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("uemail", user.Email),
                    new Claim("uid", user.Id.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _jwtToken.Issuer,
                    audience: _jwtToken.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtToken.TokenExpiry)),
                    notBefore: DateTime.Now.Subtract(TimeSpan.FromMinutes(30)),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }
        public async Task<RefreshToken> CreateRefreshToken()
        {
            return await Task.Run(() =>
            {
                string token;
                var randomNumber = new byte[32];
                //todo: add ipaddress validation
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    token = Convert.ToBase64String(randomNumber);
                }

                // Create the refresh token
                RefreshToken refreshToken = new RefreshToken()
                {
                    Token = token,
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtToken.RefreshTokenExpiry)),
                    Created = DateTime.UtcNow
                };
                return refreshToken;
            });
        }
        #region private util methods
        private async Task<User> GetUserByEmail(string email)
        {
            User result = null;
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand("GetUserByEmail");

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Email", email);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                result = ReadUser(drh);

            return result;
        }
        private async Task SaveRefreshToken(RefreshToken token, Guid userId)
        {
            string spName = "SaveRefreshToken";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);
            command.Parameters.AddWithValue("Token", token.Token);
            command.Parameters.AddWithValue("Expires", token.Expires.ToUniversalTime());
            command.Parameters.AddWithValue("Created", token.Created.ToUniversalTime());
            await command.ExecuteNonQueryAsync();
        }
        private AuthenticationDTO UserNotFound(LoginRequestDTO loginRequestDTO)
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"No Accounts Registered with {loginRequestDTO.Email}";
            return authenticationDTO;
        }

        private AuthenticationDTO UserPasswordIncorrect(LoginRequestDTO loginRequestDTO)
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"Incorrect Credentials for user {loginRequestDTO.Email}.";
            return authenticationDTO;
        }
        private User ReadUser(DataReaderHelper drh)
        {
            return new User()
            {
                Id = drh.Get<Guid>("UserId"),
                Email = drh.Get<string>("Email"),
                Username = drh.Get<string>("Username"),
                FirstName = drh.Get<string>("FirstName"),
                LastName = drh.Get<string>("LastName"),
                LastLogin = drh.Get<DateTime?>("LastLogin"),
                Locked = drh.Get<bool>("Locked"),
                DateLocked = drh.Get<DateTime?>("DateLocked")
            };
        }
        #endregion
    }
}
