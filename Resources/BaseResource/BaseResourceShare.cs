using System;
using System.ComponentModel.DataAnnotations;

namespace Resources.BaseResource
{
    public class BaseResourceShare
    {
        [StringLength (36, ErrorMessage = "欄位長度不可大於36個字元"), MinLength (36, ErrorMessage = "欄位長度不得小於36個字元")]
        public string create_user_id { get; set; }

        [StringLength (36, ErrorMessage = "欄位長度不可大於36個字元"), MinLength (36, ErrorMessage = "欄位長度不得小於36個字元")]
        public string update_user_id { get; set; }

        [Required(ErrorMessage = "Title is required."), DataType(DataType.DateTime)]
        public DateTime create_time { get; set; }

        [Required(ErrorMessage = "Title is required."), DataType(DataType.DateTime)]
        public DateTime update_time { get; set; }

        public BaseResourceShare()
        {
            this.create_time = DateTime.Now;
            this.update_time = DateTime.Now;
        }

    }
}