using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.BaseResource;

namespace Resources.Response
{
    public class CheckinLogsResource : BaseResourceShare
    {
        #region 資料欄位
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int checkin_log_id { get; set; }

        public string user_id { get; set; }

        public string ip { get; set; }

        public ForeignCheckInResource users { get; set; }

        #endregion
    }
}