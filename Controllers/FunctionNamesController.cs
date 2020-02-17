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
    public class FunctionNamesController : ControllerBase
    {
        private readonly IFunctionNamesService _FunctionNamesService;

        public FunctionNamesController(IFunctionNamesService FunctionNamesService)
        {
            _FunctionNamesService = FunctionNamesService ??
                throw new ArgumentNullException(nameof(FunctionNamesService));
        }

        /// <summary>
        /// Creates a FunctionNames.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/FunctionNames  
        ///     {
        ///        "function_name": "function_name"
        ///        "function_name_chinese": "function_name_chinese",
        ///     }
        ///
        /// </remarks>
        /// <param name="resource"></param>
        /// <returns>A newly created FunctionNames</returns>
        /// <response code="201">Returns the newly created FunctionNames</response>
        /// <response code="400">If the FunctionNames is null</response> 
        //api/FunctionNames
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<FunctionNamesModels>> CreateAsync([FromBody] InsertFunctionNamesResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _FunctionNamesService.CreateAsync(resource);
            if (!result.Success)
                return BadRequest(result.Message);


            return Ok(new { msg = "新增成功" });
        }

        /// <summary>
        /// Reads a specific FunctionNames.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET /api/FunctionNames/
        ///     {
        ///        "function_name_id": "function_name_id",
        ///        "function_name": "function_name",
        ///        "function_name_chinese": "function_name_chinese",
        ///        "create_user_id": "create_user_id",
        ///        "update_user_id": "update_user_id",
        ///        "create_time": "create_time",
        ///        "update_time": "update_time"
        ///     }
        ///
        /// </remarks>
        /// <param name="function_name"></param> 
        /// <response code="200">Returns the FunctionNames</response>
        /// <response code="404">If the FunctionNames is null</response> 
        //api/FunctionNames
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FunctionNamesResource>), 200)]
        [ProducesResponseType(typeof(IEnumerable<FunctionNamesResource>), 404)]
        public async Task<ActionResult<FunctionNamesResource>> ReadAllAsync(string function_name)
        {
            var FunctionNames = await _FunctionNamesService.ReadAllAsync(function_name);

            return Ok(FunctionNames);
        }

        /// <summary>
        /// Reads a specific FunctionNames.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET /api/FunctionNames/
        ///     {
        ///        "function_name_id": "function_name_id",
        ///        "function_name": "function_name",
        ///        "function_name_chinese": "function_name_chinese",
        ///        "create_user_id": "create_user_id",
        ///        "update_user_id": "update_user_id",
        ///        "create_time": "create_time",
        ///        "update_time": "update_time"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param> 
        /// <response code="200">Returns the FunctionNames</response>
        /// <response code="404">If the FunctionNames is null</response> 
        //api/FunctionNames/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FunctionNamesResource), 200)]
        [ProducesResponseType(typeof(FunctionNamesResource), 404)]
        public async Task<ActionResult<FunctionNamesResource>> ReadOneAsync(int id)
        {
            var FunctionNames = await _FunctionNamesService.ReadOneAsync(id);

            if (FunctionNames == null)
                return NotFound();

            return Ok(FunctionNames);
        }

        /// <summary>
        /// Updates a FunctionNames.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/FunctionNames/{id}
        ///     {
        ///        "function_name": "function_name",
        ///        "function_name_chinese": "function_name_chinese",
        ///        "update_user_id": "update_user_id",
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns>A newly updated FunctionNames</returns>
        /// <response code="200">Returns the newly updated FunctionNames</response>
        /// <response code="400">If the FunctionNames is null</response>
        //api/FunctionNames/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateFunctionNamesResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _FunctionNamesService.UpdateAsync(id, resource);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { msg = "更新成功!" });
        }

        /// <summary>
        /// Deletes a specific FunctionNames.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly dalete FunctionNames</response>
        /// <response code="400">If the FunctionNames is null</response>
        //api/FunctionNames/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Boolean check = await _FunctionNamesService.DeleteAsync(id);

            if (check == false)
                return BadRequest(new { err = "刪除錯誤" });

            return Ok(new { msg = "刪除成功" });
        }
    }
}