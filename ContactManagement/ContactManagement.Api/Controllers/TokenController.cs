using ContactManagement.Application.Dtos;
using ContactManagement.Application.Interfaces;
using ContactManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.Api.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var user = new User(0, userDto.Username, userDto.Password);
            var token = await _tokenService.GetToken(user);

            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized("Invalid username or password.");

            return Ok(token);
        }

    }
}
