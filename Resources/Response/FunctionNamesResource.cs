using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.BaseResource;

namespace Resources.Response
{
    public class FunctionNamesResource : BaseResourceShare
    {
        #region 資料欄位
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int function_name_id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string function_name { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string function_name_chinese { get; set; }
        #endregion
    }
}