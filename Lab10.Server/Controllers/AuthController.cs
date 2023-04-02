using Lab10.Server.Dtos;
using Lab10.Server.Models;
using Lab10.Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab10.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<Response> Register([FromBody] User request)
        {
            return await _service.Register(request);
        }

        [HttpPost("login")]
        public async Task<Response> Login([FromBody] User request)
        {
            return await _service.Login(request);
        }
    }
}
