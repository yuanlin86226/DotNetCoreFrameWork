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
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _RolesService;

        public RolesController(IRolesService RolesService)
        {
            _RolesService = RolesService ??
                throw new ArgumentNullException(nameof(RolesService));
        }

        /// <summary>
        /// Creates a Roles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Roles  
        ///     {
        ///        "role": "role",
        ///        "create_user_id": "create_user_id",
        ///        "update_user_id": "update_user_id"
        ///     }
        ///
        /// </remarks>
        /// <param name="resource"></param>
        /// <returns>A newly created Roles</returns>
        /// <response code="201">Returns the newly created Roles</response>
        /// <response code="400">If the Roles is null</response> 
        //api/Roles
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RolesModels>> CreateAsync([FromBody] InsertRolesResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            var result = await _RolesService.CreateAsync(resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { msg = "新增成功" });
        }

        /// <summary>
        /// Reads a specific Roles.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET /api/Roles/
        ///     [
        ///       {
        ///         "role_id": role_id,
        ///         "role": "role",
        ///         "permissions": [
        ///           {
        ///             "function_names": {
        ///               "function_name_id": function_name_id,
        ///               "function_name": "function_name",
        ///               "function_name_chinese": "function_name_chinese"
        ///             },
        ///             "actions": [
        ///               {
        ///                 "action_id": action_id,
        ///                 "action": "action"
        ///               } 
        ///             ]
        ///           }
        ///         ],
        ///         "create_time": "2019-11-28T10:05:50.9791833",
        ///         "update_time": "2019-11-28T10:05:50.979184"
        ///       }
        ///     ]
        ///
        /// </remarks>
        /// <param name="role"></param> 
        /// <response code="200">Returns the Roles</response>
        /// <response code="404">If the Roles is null</response> 
        //api/Roles
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RolesResource>), 200)]
        [ProducesResponseType(typeof(IEnumerable<RolesResource>), 404)]
        public async Task<ActionResult<RolesResource>> ReadAllAsync(string role)
        {
            return Ok(await _RolesService.ReadAllAsync(role));
        }

        /// <summary>
        /// Reads a specific Roles.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET /api/Roles/{id}
        ///     [
        ///       {
        ///         "role_id": role_id,
        ///         "role": "role",
        ///         "permissions": [
        ///           {
        ///             "function_names": {
        ///               "function_name_id": function_name_id,
        ///               "function_name": "function_name",
        ///               "function_name_chinese": "function_name_chinese"
        ///             },
        ///             "actions": [
        ///               {
        ///                 "action_id": action_id,
        ///                 "action": "action"
        ///               } 
        ///             ]
        ///           }
        ///         ],
        ///         "create_time": "2019-11-28T10:05:50.9791833",
        ///         "update_time": "2019-11-28T10:05:50.979184"
        ///       }
        ///     ]
        ///
        /// </remarks>
        /// <param name="id"></param> 
        /// <response code="200">Returns the Roles</response>
        /// <response code="404">If the Roles is null</response> 
        //api/Roles/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RolesResource), 200)]
        [ProducesResponseType(typeof(RolesResource), 404)]
        public async Task<ActionResult<RolesResource>> ReadOneAsync(int id)
        {
            var Roles = await _RolesService.ReadOneAsync(id);

            if (Roles == null)
                return NotFound();

            return Ok(Roles);
        }

        /// <summary>
        /// Updates a Roles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Roles/{id}
        ///     {
        ///        "role": "role",
        ///        "update_user_id": "update_user_id"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns>A newly updated Roles</returns>
        /// <response code="200">Returns the newly updated Roles</response>
        /// <response code="400">If the Roles is null</response>
        //api/Roles/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateRolesResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _RolesService.UpdateAsync(id, resource);
            if (!result.Success)
                return BadRequest(result.Message);

            
            return Ok(new { msg = "更新成功" });
        }

        /// <summary>
        /// Deletes a specific Roles.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly dalete Roles</response>
        /// <response code="400">If the Roles is null</response>
        //api/Roles/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Boolean check = await _RolesService.DeleteAsync(id);

            if (check == false)
                return BadRequest(new { err = "刪除錯誤" });

            return Ok(new { msg = "刪除成功" });
        }
    }
}