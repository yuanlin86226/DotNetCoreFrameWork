using Models.BaseFfiledModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("checkin_log")]
    public class CheckInLogModels : BaseInformationShare //打卡紀錄
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int checkin_log_id { get; set; }

        public string user_id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime time { get; set; }

        public string ip { get; set; }


        [ForeignKey("user_id")]
        public virtual UsersModels users { get; set; }

        [ForeignKey("create_user_id"), InverseProperty("RolePermissionsCreateUser")]
        public virtual UsersModels create_user { get; set; }

        [ForeignKey("update_user_id"), InverseProperty("RolePermissionsUpdateUser")]
        public virtual UsersModels update_user { get; set; }

    }
}