using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.BaseResource;

namespace Resources.Response
{
    public class ActionsResource : BaseResourceShare
    {
        #region 資料欄位
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int action_id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string action { get; set; }

        #endregion
    }
}