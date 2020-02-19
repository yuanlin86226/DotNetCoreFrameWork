using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("users")]
    public class UsersModels //使用者
    {
        #region 資料欄位
        [Key, StringLength(36, ErrorMessage = "欄位長度不得大於36個字元"), MinLength(36, ErrorMessage = "欄位長度不得小於36個字元")]
        public string user_id { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(20, ErrorMessage = "欄位長度不得大於20個字元")]
        public string account_number { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(100, ErrorMessage = "欄位長度不得大於100個字元")]
        public string password { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(10, ErrorMessage = "欄位長度不得大於10個字元")]
        public string user_name { get; set; }

        public Nullable<int> role_id { get; set; }

        [Phone]
        public string phone { get; set; }

        [EmailAddress(ErrorMessage = "E-Mail欄位驗證錯誤")]
        public string email { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(1, ErrorMessage = "欄位長度不可大於1個字元"), MinLength(1, ErrorMessage = "欄位長度不得小於1個字元")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Title is required."), DataType(DataType.DateTime)]
        public DateTime due_date { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<DateTime> resignation_date { get; set; }

        [Required(ErrorMessage = "Title is required."), DataType(DataType.DateTime)]
        public DateTime create_date { get; set; }
        #endregion

        #region 外來鍵
        [ForeignKey("role_id")]
        public virtual RolesModels roles { get; set; }

        #endregion

        #region 外部關聯
        public virtual ICollection<ActionsModels> ActionsCreateUser { get; set; }
        public virtual ICollection<ActionsModels> ActionsUpdateUser { get; set; }
        public virtual ICollection<FunctionNamesModels> FunctionNamesCreateUser { get; set; }
        public virtual ICollection<FunctionNamesModels> FunctionNamesUpdateUser { get; set; }
        public virtual ICollection<PermissionsModels> PermissionsCreateUser { get; set; }
        public virtual ICollection<PermissionsModels> PermissionsUpdateUser { get; set; }
        public virtual ICollection<RolePermissionsModels> RolePermissionsCreateUser { get; set; }
        public virtual ICollection<RolePermissionsModels> RolePermissionsUpdateUser { get; set; }
        public virtual ICollection<RolesModels> RolesCreateUser { get; set; }
        public virtual ICollection<RolesModels> RolesUpdateUser { get; set; }
        public virtual ICollection<CheckinLogsModels> CheckinLogsCreateUser { get; set; }
        public virtual ICollection<CheckinLogsModels> CheckinLogsUpdateUser { get; set; }
        public virtual ICollection<CheckinLogsModels> CheckinLogs { get; set; }
        #endregion
    }
}