using Models.BaseFfiledModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("actions")]
    public class ActionsModels : BaseInformationShare  //功能名稱
    {
        #region 資料欄位
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int action_id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string action { get; set; }

        #endregion

        #region 外來鍵
        [ForeignKey("create_user_id"), InverseProperty("ActionsCreateUser")]
        public virtual UsersModels create_user { get; set; }

        [ForeignKey("update_user_id"), InverseProperty("ActionsUpdateUser")]
        public virtual UsersModels update_user { get; set; }
        #endregion

        #region 外部關聯
        public virtual ICollection<PermissionsModels> Permissions { get; set; }
        #endregion
    }
}