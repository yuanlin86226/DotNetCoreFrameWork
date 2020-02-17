using System;
using Resources.BaseResource;

namespace Resources.Request
{
    public class UpdatePermissionsResource : BaseUpdateResourceShare
    {
        #region 資料欄位
        public Nullable<int> function_name_id { get; set; }

        public Nullable<int> action_id { get; set; }
        #endregion
    }
}