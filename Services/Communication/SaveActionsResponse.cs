using Models;

namespace Services.Communication {
    public class SaveActionsResponse : BaseResponse {
        public ActionsModels _Actions { get; private set; }

        private SaveActionsResponse (bool success, string message, ActionsModels Actions) : base (success, message) {
            _Actions = Actions;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="Actions">Saved Actions.</param>
        /// <returns>Response.</returns>
        public SaveActionsResponse (ActionsModels Actions) : this (true, string.Empty, Actions) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveActionsResponse (string message) : this (false, message, null) { }
    }
}