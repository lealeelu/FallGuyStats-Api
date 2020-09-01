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
            _statService.CheckPlayerLog();

            var statDTO = new StatDTO()
            {
                TodayStats = new SessionStatDTO
                {
                    Crowns = 5,
                    Episodes = 10,
                    Cheaters = 1
                },
                SeasonStats = new SessionStatDTO
                {
                    Crowns = 69,
                    Episodes = 420,
                    Cheaters = 42
                }
            };

            return statDTO;
        }
    }
}
