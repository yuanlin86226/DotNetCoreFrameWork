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
    public class ActionsController : ControllerBase
    {
        private readonly IActionsService _ActionsService;

        public ActionsController(IActionsService ActionsService)
        {
            _ActionsService = ActionsService ??
                throw new ArgumentNullException(nameof(ActionsService));
        }

        /// <summary>
        /// Creates a Actions.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Actions  
        ///     {
        ///        "action": "action"
        ///     }
        ///
        /// </remarks>
        /// <param name="resource"></param>
        /// <returns>A newly created Actions</returns>
        /// <response code="201">Returns the newly created Actions</response>
        /// <response code="400">If the Actions is null</response> 
        //api/Actions
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ActionsModels>> CreateAsync([FromBody] InsertActionsResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _ActionsService.CreateAsync(resource);
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
        /// <param name="action"></param> 
        /// <response code="200">Returns the Actions</response>
        /// <response code="404">If the Actions is null</response> 
        //api/Actions
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActionsResource>), 200)]
        [ProducesResponseType(typeof(IEnumerable<ActionsResource>), 404)]
        public async Task<ActionResult<ActionsResource>> ReadAllAsync(string action)
        {
            var Actions = await _ActionsService.ReadAllAsync(action);
            return Ok(Actions);
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
        /// <param name="id"></param> 
        /// <response code="200">Returns the Actions</response>
        /// <response code="404">If the Actions is null</response> 
        //api/Actions/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionsModels), 200)]
        [ProducesResponseType(typeof(ActionsModels), 404)]
        public async Task<ActionResult<ActionsModels>> ReadOneAsync(int id)
        {
            var Actions = await _ActionsService.ReadOneAsync(id);

            if (Actions == null)
                return NotFound();

            return Ok(Actions);
        }

        /// <summary>
        /// Updates a Actions.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Actions/{id}
        ///     {
        ///        "action": "action",
        ///        "update_user_id": "update_user_id",
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns>A newly updated Actions</returns>
        /// <response code="200">Returns the newly updated Actions</response>
        /// <response code="400">If the Actions is null</response>
        //api/Actions/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateActionsResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _ActionsService.UpdateAsync(id, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { msg = "更新成功!" });
        }

        /// <summary>
        /// Deletes a specific Actions.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly dalete Actions</response>
        /// <response code="400">If the Actions is null</response>
        //api/Actions/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Boolean check = await _ActionsService.DeleteAsync(id);

            if (check == false)
                return BadRequest(new { err = "刪除錯誤" });

            return Ok(new { msg = "刪除成功" });
        }
    }
}