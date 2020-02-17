using Models.BaseFfiledModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("role_permissions")]
    public class RolePermissionsModels : BaseInformationShare //角色權限
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_permission_id { get; set; }

        public Nullable<int> role_id { get; set; }

        public Nullable<int> permission_id { get; set; }

        [ForeignKey("role_id")]
        public virtual RolesModels roles { get; set; }

        [ForeignKey("permission_id")]
        public virtual PermissionsModels permissions { get; set; }
        [ForeignKey("create_user_id"), InverseProperty("RolePermissionsCreateUser")]
        public virtual UsersModels create_user { get; set; }

        [ForeignKey("update_user_id"), InverseProperty("RolePermissionsUpdateUser")]
        public virtual UsersModels update_user { get; set; }

    }
}