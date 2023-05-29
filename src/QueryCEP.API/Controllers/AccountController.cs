using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QueryCEP.API.Dtos;
using QueryCEP.API.Entities;
using QueryCEP.API.Services;

namespace QueryCEP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("roles/add")]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
        {
            var result = await _accountService.CreateRoleAsync(request);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _accountService.RegisterAsync(request);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _accountService.LoginAsync(request);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}