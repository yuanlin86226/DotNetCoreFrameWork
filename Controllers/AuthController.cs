using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resources.Request;
using Services.IServices;

namespace Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService AuthService)
        {
            this._AuthService = AuthService ??
                throw new ArgumentNullException(nameof(AuthService));
        }

        /// <summary>
        /// Checkud a specific Users.
        /// </summary>
        /// <remarks>
        /// Sample Login:
        ///
        ///     POST /api/Auth/Login
        ///     {
        ///        "account_number": "account_number",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <param name="resource"></param>
        /// <returns>Login Actions</returns>
        /// <response code="200">Returns the Login Action</response>
        /// <response code="404">If the Actions is null</response> 
        /// api/Auth/Login
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginResource), 200)]
        [ProducesResponseType(typeof(LoginResource), 404)]
        public async Task<ActionResult<LoginResource>> LoginAsync(LoginResource resource)
        {
            var LoginOutput = await _AuthService.LoginAsync(resource);
            
            if (string.IsNullOrEmpty(LoginOutput.user_id))
                return NotFound(new {msg = "登入失敗"});

            return Ok(LoginOutput);
        }

    }
}