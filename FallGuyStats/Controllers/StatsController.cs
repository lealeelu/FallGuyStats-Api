using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FallGuyStats.Objects.DTOs;
using FallGuyStats.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FallGuyStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly StatService _statService;

        public StatsController(
            StatService statService
        )
        {
            _statService = statService;
        }

        // GET: api/Stats
        [HttpGet]
        public ActionResult<StatDTO> GetStats()
        {
            return _statService.GetStats();
        }
    }
}
