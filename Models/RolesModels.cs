using Models.BaseFfiledModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("roles")]
    public class RolesModels : BaseInformationShare //角色
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(10, ErrorMessage = "欄位長度不得大於10個字元")]
        public string role { get; set; }

        [ForeignKey("create_user_id"), InverseProperty("RolesCreateUser")]
        public virtual UsersModels create_user { get; set; }

        [ForeignKey("update_user_id"), InverseProperty("RolesUpdateUser")]
        public virtual UsersModels update_user { get; set; }

        public virtual ICollection<UsersModels> Users { get; set; }
        public virtual ICollection<RolePermissionsModels> RolePermissions { get; set; }

    }
}