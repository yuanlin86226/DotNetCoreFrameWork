using Models.BaseFfiledModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("permissions")]
    public class PermissionsModels : BaseInformationShare //權限
    {
        #region 資料欄位
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int permission_id { get; set; }

        public Nullable<int> function_name_id { get; set; }

        public Nullable<int> action_id { get; set; }
        #endregion

        #region 外來鍵
        [ForeignKey("function_name_id")]
        public virtual FunctionNamesModels function_names { get; set; }

        [ForeignKey("action_id")]
        public virtual ActionsModels actions { get; set; }
        
        [ForeignKey("create_user_id"), InverseProperty("PermissionsCreateUser")]
        public virtual UsersModels create_user { get; set; }

        [ForeignKey("update_user_id"), InverseProperty("PermissionsUpdateUser")]
        public virtual UsersModels update_user { get; set; }
        #endregion

        #region 外部關聯
        public virtual ICollection<RolePermissionsModels> RolePermissions { get; set; }
        #endregion
    }
}