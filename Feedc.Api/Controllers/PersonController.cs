using System;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Feedc.Domain.UserManagement;
using Feedc.Domain.PersonManagement;
using Microsoft.AspNetCore.Identity;
using Feedc.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Feedc.Application.Query.PersonQueries;
using Feedc.Application.Command.PersonCommands;

namespace Feedc.Api.Controllers
{
    [Produces("application/json")]
    [Route("feedcperson/")]
    public class PersonController : Controller
    {
        private QueryExecutor _queryExecutor;
        private CommandExecutor _commandExecutor;

        public PersonController(QueryExecutor queryExecutor, CommandExecutor commandExecutor, UserManager<User> userManager)
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }

        [HttpPost]
        [Route("createperson")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePersonCommand command)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            if (user == null)
                return BadRequest();

            IList<Claim> claim = user.Claims.ToList();
            command.UserId = Convert.ToInt32(claim[1].Value);

            var result = await _commandExecutor.ExecuteAsync(command);

            if (!result.Success)
                return BadRequest(new { success = false });

            return Ok(new { success = true });
        }

        [HttpPost]
        [Route("deleteperson")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromBody] DeletePersonCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            if (!result.Success)
                return BadRequest(new { success = false });

            return Ok(new { success = true });
        }

        [HttpPost]
        [Route("boundphonenumber")]
        [Authorize]
        public async Task<IActionResult> BoundPhoneNumber([FromBody] AddPersonPhoneCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            if (!result.Success)
                return BadRequest(new { success = false });

            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("getperson")]
        [Authorize]
        public async Task<IActionResult> GetPerson([FromBody] GetPersonQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<GetPersonQuery, Person>(query);

            if (!result.Success)
                return BadRequest(new { success = false });

            return Ok(new
            {
                data = JsonConvert.SerializeObject(new
                {
                    firstName = result.Data.FirstName,
                    Phone = result.Data.Phone
                })
            });
        }

        [HttpGet]
        [Route("getpersons")]
        [Authorize]
        public async Task<IActionResult> GetPersons([FromQuery] GetPersonsPhonesQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<GetPersonsPhonesQuery, List<Person>>(query);

            if (!result.Success)
                return BadRequest(new { success = false });

            return Ok(new
            {
                data = JsonConvert.SerializeObject(result.Data.Select(data => new
                {
                    firstName = data.FirstName,
                    Phone = data.Phone
                }))
            });
        }
    }
}
