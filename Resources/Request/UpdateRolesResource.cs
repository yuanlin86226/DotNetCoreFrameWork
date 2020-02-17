using System.ComponentModel.DataAnnotations;
using Resources.BaseResource;

namespace Resources.Request
{
    public class UpdateRolesResource : BaseUpdateResourceShare
    {
        #region  資料欄位
        [Required(ErrorMessage = "Title is required."), StringLength(10, ErrorMessage = "欄位長度不得大於10個字元")]
        public string role { get; set; }

        public int[] permission_id { get; set; }
        #endregion
    }
}