using System.ComponentModel.DataAnnotations;

namespace Resources.Request
{
    public class LoginResource
    {
        #region  資料欄位
        [Required(ErrorMessage = "Title is required."), StringLength(20, ErrorMessage = "欄位長度不得大於20個字元")]
        public string account_number { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(20, ErrorMessage = "欄位長度不得大於20個字元")]
        public string password { get; set; }
        #endregion
    }
}