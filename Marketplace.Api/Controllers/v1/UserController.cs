using Marketplace.Api.Attributes;
using Marketplace.Application.Contracts;
using Marketplace.Application.Data.Shared;
using Marketplace.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers.v1
{
    [BearerAuthorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : DefaultController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>Retorna OK como estado de sucesso da chamada do controlador.</summary>
        [AllowAnonymous]
        [HttpGet("status")]
        public IActionResult Status()
            => ConvertData(StatusService.OkStatus());

        /// <summary>Cria um novo usuário.</summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserPostRequest request)
            => ConvertData(await _userService.Create(request, CurrentUserAccessLevel ?? AccessLevelType.Anonymous));

        /// <summary>Executa a entrada no sistema de um usuário cadastrado.</summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
            => ConvertData(await _userService.Login(request));        
    }
}
