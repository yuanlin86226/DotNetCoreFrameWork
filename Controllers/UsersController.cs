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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _UsersService;

        public UsersController(IUsersService UsersService)
        {
            _UsersService = UsersService ??
                throw new ArgumentNullException(nameof(UsersService));
        }

        /// <summary>
        /// Creates a Users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Users  
        ///     {
        ///        "account_number": "account_number",
        ///        "password": "password",
        ///        "user_name": "user_name",
        ///        "role_id": "role_id",
        ///        "phone": "phone",
        ///        "email": "email",
        ///        "gender": "gender",
        ///        "due_date": "due_date"
        ///     }
        ///
        /// </remarks>
        /// <param name="resource"></param>
        /// <returns>A newly created Users</returns>
        /// <response code="201">Returns the newly created Users</response>
        /// <response code="400">If the Users is null</response> 
        //api/Users
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UsersModels>> CreateAsync([FromBody] InsertUsersResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _UsersService.CreateAsync(resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { msg = "新增成功" });
        }

        /// <summary>
        /// Reads a specific Users.
        /// </summary>
        /// <remarks>
        /// Sample Users:
        ///
        ///     GET /api/Users/
        ///     [
        ///         {
        ///            "user_id": "user_id"
        ///            "account_number": "account_number",
        ///            "user_name": "user_name",
        ///            "role"{
        ///                 "role_id": "role_id",
        ///                 "role": "role",
        ///            },
        ///            "phone": "phone",
        ///            "email": "email",
        ///            "gender": "gender",
        ///            "due_date": "due_date",
        ///            "resignation_date": "resignation_date",
        ///            "create_date": "create_date"
        ///         }
        ///     ]
        ///
        /// </remarks>
        /// <param name="user_name"></param>
        /// <param name="account_number"></param> 
        /// <response code="200">Returns the Users</response>
        /// <response code="404">If the Users is null</response> 
        //api/Users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsersResource>), 200)]
        [ProducesResponseType(typeof(IEnumerable<UsersResource>), 404)]
        public async Task<ActionResult<UsersResource>> ReadAllAsync(string account_number, string user_name)
        {
            IEnumerable<UsersResource> Users = await _UsersService.ReadAllAsync(account_number, user_name);

            return Ok(Users);
        }

        /// <summary>
        /// Reads a specific Users.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET /api/Users/
        ///     {
        ///        "user_id": "user_id"
        ///        "account_number": "account_number",
        ///        "user_name": "user_name",
        ///        "role"{
        ///             "role_id": "role_id",
        ///             "role": "role",
        ///        },
        ///        "phone": "phone",
        ///        "email": "email",
        ///        "gender": "gender",
        ///        "due_date": "due_date",
        ///        "resignation_date": "resignation_date",
        ///        "create_date": "create_date"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param> 
        /// <response code="200">Returns the Users</response>
        /// <response code="404">If the Users is null</response> 
        //api/Users/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsersResource), 200)]
        [ProducesResponseType(typeof(UsersResource), 404)]
        public async Task<ActionResult<UsersResource>> ReadOneAsync(string id)
        {
            var Users = await _UsersService.ReadOneAsync(id);

            if (Users == null)
                return NotFound();

            return Ok(Users);
        }

        /// <summary>
        /// Updates a Users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Users/{id}
        ///     {
        ///        "password": "password",
        ///        "user_name": "user_name",
        ///        "role_id": "role_id",
        ///        "phone": "phone",
        ///        "email": "email",
        ///        "gender": "gender",
        ///        "due_date": "due_date",
        ///        "resignation_date": "resignation_date"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns>A newly updated Users</returns>
        /// <response code="200">Returns the newly updated Users</response>
        /// <response code="400">If the Users is null</response>
        //api/Users/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateUsersResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _UsersService.UpdateAsync(id, resource);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { msg = "更新成功" });
        }

        /// <summary>
        /// Deletes a specific Users.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly dalete Users</response>
        /// <response code="400">If the Users is null</response>
        //api/Users/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Boolean check = await _UsersService.DeleteAsync(id);

            if (check == false)
                return BadRequest(new { err = "刪除錯誤" });

            return Ok(new { msg = "刪除成功" });
        }
    }
}