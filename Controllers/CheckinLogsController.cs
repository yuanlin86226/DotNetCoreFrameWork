using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Resources.Response;
using Resources.Request;
using Services.IServices;

namespace Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CheckinLogsController : ControllerBase
    {
        private readonly ICheckinLogsService _CheckinLogsService;

        public CheckinLogsController(ICheckinLogsService CheckinLogsService)
        {
            _CheckinLogsService = CheckinLogsService ??
                throw new ArgumentNullException(nameof(CheckinLogsService));
        }

        /// <summary>
        /// Creates a CheckinLogs.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/CheckinLogs  
        ///     {
        ///        "ip": "ip"
        ///        "user_id": "user_id"
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created CheckinLogs</returns>
        /// <response code="201">Returns the newly created CheckinLogs</response>
        /// <response code="400">If the CheckinLogs is null</response> 
        //api/CheckinLogs
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CheckinLogsModels>> CreateAsync()
        {
            var result = await _CheckinLogsService.CreateAsync();
            if (!result.Success)
               return BadRequest(result.Message);


            return Ok(new { msg = "新增成功" });
        }

        /// <summary>
        /// Reads a specific Actions.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET /api/Actions/
        ///     {
        ///        "action_id": "action_id",
        ///        "action": "action",
        ///        "create_user_id": "create_user_id",
        ///        "update_user_id": "update_user_id",
        ///        "create_time": "create_time",
        ///        "update_time": "update_time"
        ///     }
        ///
        /// </remarks>
        /// <param name="log"></param> 
        /// <response code="200">Returns the Actions</response>
        /// <response code="404">If the Actions is null</response> 
        //api/Actions
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CheckinLogsResource>), 200)]
        [ProducesResponseType(typeof(IEnumerable<CheckinLogsResource>), 404)]
        public async Task<ActionResult<CheckinLogsResource>> ReadAllAsync(string log)
        {
            var CheckinLogs = await _CheckinLogsService.ReadAllAsync(log);
            return Ok(CheckinLogs);
        }
    }
}