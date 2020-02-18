using Models;

namespace Services.Communication {
    public class SaveCheckinLogsResponse : BaseResponse {
        public CheckinLogsModels _CheckinLogs { get; private set; }

        private SaveCheckinLogsResponse (bool success, string message, CheckinLogsModels CheckinLogs) : base (success, message) {
            _CheckinLogs = CheckinLogs;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="CheckinLogs">Saved Actions.</param>
        /// <returns>Response.</returns>
        public SaveCheckinLogsResponse (CheckinLogsModels CheckinLogs) : this (true, string.Empty, CheckinLogs) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveCheckinLogsResponse (string message) : this (false, message, null) { }
    }
}