using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.BaseResource;

namespace Resources.Response
{
    public class PermissionsResource : BaseResourceShare
    {
        #region 資料欄位
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int permission_id { get; set; }
        
        #endregion

        #region 外部關聯
        public virtual List<ActionsResource> actions { get; set; }
        public virtual FunctionNamesResource function_names { get; set; }
        #endregion
    }
}