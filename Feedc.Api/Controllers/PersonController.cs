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
        public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
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
        public async Task<IActionResult> Delete([FromBody] DeletePersonCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            if (!result.Success)
                return BadRequest(new { success = false });

            return Ok(new { success = true });
        }

        [HttpPost]
        [Route("addphonenumber")]
        [Authorize]
        public async Task<IActionResult> AddPhoneNumber([FromBody] AddPersonPhoneCommand command)
        {
            var result = await _commandExecutor.ExecuteAsync(command);

            if (!result.Success)
                return BadRequest(new { success = false });

            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("personbyphone")]
        [Authorize]
        public async Task<IActionResult> GetPersonByPhone([FromBody] GetPersonByPhoneQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<GetPersonByPhoneQuery, Person>(query);

            if (!result.Success)
                return BadRequest(new { success = false });

            string serialized = JsonConvert.SerializeObject(new
            {
                firstName = result.Data.FirstName,
                Phone = result.Data.Phone
            });

            return Ok(new { data = serialized });
        }

        [HttpGet]
        [Route("phonelisting")]
        [Authorize]
        public async Task<IActionResult> PersonsPhonesListing([FromQuery] GetPersonsPhonesQuery query)
        {
            var result = await _queryExecutor.ExecuteAsync<GetPersonsPhonesQuery, List<Person>>(query);

            if (!result.Success)
                return BadRequest(new { success = false });

            string serialized = JsonConvert.SerializeObject(result.Data.Select(data => new
            {
                firstName = data.FirstName,
                Phone = data.Phone
            }));

            return Ok(new { data = serialized });
        }
    }
}
