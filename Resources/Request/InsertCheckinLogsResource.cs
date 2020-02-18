using System.ComponentModel.DataAnnotations;
using Resources.BaseResource;

namespace Resources.Request
{
    public class InsertCheckinLogsResource : BaseResourceShare
    {
        #region 資料欄位

        public string user_id { get; set; }

        public string ip { get; set; }
        
        #endregion
    }
}