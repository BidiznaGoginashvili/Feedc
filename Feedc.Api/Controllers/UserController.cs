using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Feedc.Domain.UserManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Feedc.Application.Infrastructure;
using Microsoft.Extensions.Configuration;
using Feedc.Application.Query.UserQueries;
using Feedc.Application.UserCommands.Command;

namespace Feedc.Api.Controllers
{
    [Produces("application/json")]
    [Route("feedcuser/")]
    public class UserController : Controller
    {
        private IConfiguration _config;
        private QueryExecutor _queryExecutor;
        private CommandExecutor _commandExecutor;

        public UserController(IConfiguration config,
               QueryExecutor queryExecutor,
               CommandExecutor commandExecutor)
        {
            _config = config;
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromBody] CreateUserCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            if (!result.Success)
                return BadRequest();

            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<AuthenticateUserQuery, User>(query);

            if (result.Success)
            {
                var token = GenerateJsonWebToken(result.Data);
                return Ok(new { token = token });
            }

            return BadRequest(new { success = false });
        }

        private string GenerateJsonWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
    }
}
