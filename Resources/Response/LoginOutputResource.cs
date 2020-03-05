using System.ComponentModel.DataAnnotations;

namespace Resources.Response
{
    public class LoginOutputResource
    {
        [Key, StringLength(36, ErrorMessage = "欄位長度不得大於36個字元"), MinLength(36, ErrorMessage = "欄位長度不得小於36個字元")]
        public string user_id { get; set; }
        public string user_name { get; set; }

        [Required(ErrorMessage = "Title is required."), StringLength(10, ErrorMessage = "欄位長度不得大於10個字元")]
        public string role { get; set; }

        public string JWTKey { get; set; }

        public virtual PermissionsResource[] Permissions { get; set; }
    }
}