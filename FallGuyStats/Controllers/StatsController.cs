// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FallGuyStats.Objects.DTOs;
using FallGuyStats.Services;
using Microsoft.AspNetCore.Cors;
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
        [ProducesResponseType(typeof(StatDTO), StatusCodes.Status200OK)]
        public ActionResult<StatDTO> GetStats()
        {
            return _statService.GetStats();
        }
    }
}
