using Models;

namespace Services.Communication {
    public class SavePermissionsResponse : BaseResponse {
        public PermissionsModels _Permission { get; private set; }

        private SavePermissionsResponse (bool success, string message, PermissionsModels Permission) : base (success, message) {
            _Permission = Permission;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="Permission">Saved Actions.</param>
        /// <returns>Response.</returns>
        public SavePermissionsResponse (PermissionsModels Permission) : this (true, string.Empty, Permission) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SavePermissionsResponse (string message) : this (false, message, null) { }
    }
}