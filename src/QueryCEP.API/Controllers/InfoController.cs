using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueryCEP.API.Entities;
using QueryCEP.API.Services;

namespace QueryCEP.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly IInfoService _infoService;
        private readonly ICacheService _cacheService;
        public InfoController(IInfoService infoService, ICacheService cacheService)
        {
            _infoService = infoService;
            _cacheService = cacheService;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<Cep>> GetInfoAsync(string cep)
        {
            var cachedData = _cacheService.GetData<Cep>("ceps");
            if (cachedData != null) return Ok(cachedData);

            cachedData = await _infoService.ProcessInfo(cep);
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<Cep>("ceps", cachedData, expiryTime);

            return Ok(cachedData);
        }
    }
}