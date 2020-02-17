using System.ComponentModel.DataAnnotations;
using Resources.BaseResource;

namespace Resources.Request
{
    public class InsertFunctionNamesResource : BaseResourceShare
    {
        #region 資料欄位
        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string function_name { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string function_name_chinese { get; set; }
        #endregion
    }
}