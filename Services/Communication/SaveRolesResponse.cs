using Models;

namespace Services.Communication
{
    public class SaveRolesResponse : BaseResponse
    {

        private SaveRolesResponse(bool success, string message) : base(success, message)
        {

        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="check">Saved Roles.</param>
        /// <returns>Response.</returns>
        public SaveRolesResponse(bool check) : this(check, string.Empty) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveRolesResponse(string message) : this(false, message) { }
    }
}