using Models.BaseFfiledModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("function_names")]
    public class FunctionNamesModels : BaseInformationShare  //功能名稱
    {
        #region 資料欄位
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int function_name_id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string function_name { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於50個字元")]
        public string function_name_chinese { get; set; }
        #endregion

        #region 外部關聯
        public virtual ICollection<PermissionsModels> Permissions { get; set; }
        
        [ForeignKey("create_user_id"), InverseProperty("FunctionNamesCreateUser")]
        public virtual UsersModels create_user { get; set; }

        [ForeignKey("update_user_id"), InverseProperty("FunctionNamesUpdateUser")]
        public virtual UsersModels update_user { get; set; }
        #endregion
    }
}