using Models;

namespace Services.Communication
{
    public class SaveUsersResponse : BaseResponse
    {
        public UsersModels _Users { get; private set; }

        private SaveUsersResponse(bool success, string message, UsersModels Users) : base(success, message)
        {
            _Users = Users;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="Users">Saved Users.</param>
        /// <returns>Response.</returns>
        public SaveUsersResponse(UsersModels Users) : this(true, string.Empty, Users) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveUsersResponse(string message) : this(false, message, null) { }
    }
}