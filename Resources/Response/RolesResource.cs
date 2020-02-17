using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.BaseResource;

namespace Resources.Response
{
    public class RolesResource : BaseResourceShare
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(10, ErrorMessage = "欄位長度不得大於10個字元")]
        public string role { get; set; }

        public virtual PermissionsResource[] Permissions { get; set; }
    }
}