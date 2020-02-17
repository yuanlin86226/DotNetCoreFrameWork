using System;
using System.ComponentModel.DataAnnotations;

namespace Resources.Request
{
    public class UpdateUsersResource
    {
        #region 資料欄位
        [StringLength(20, ErrorMessage = "欄位長度不得大於20個字元")]
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

        public Boolean UpdatePasswordChecked { get; set; }
        #endregion
    }
}