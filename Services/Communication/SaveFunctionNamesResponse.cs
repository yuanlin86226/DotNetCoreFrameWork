using Models;

namespace Services.Communication {
    public class SaveFunctionNamesResponse : BaseResponse {
        public FunctionNamesModels _FunctionNames { get; private set; }

        private SaveFunctionNamesResponse (bool success, string message, FunctionNamesModels FunctionNames) : base (success, message) {
            _FunctionNames = FunctionNames;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="FunctionNames">Saved Actions.</param>
        /// <returns>Response.</returns>
        public SaveFunctionNamesResponse (FunctionNamesModels FunctionNames) : this (true, string.Empty, FunctionNames) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveFunctionNamesResponse (string message) : this (false, message, null) { }
    }
}