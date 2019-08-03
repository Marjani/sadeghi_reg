using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Registration.Web.Models
{
    public class FollowUpViewModel
    {
        [MaxLength(15, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "کد پیگیری")]
        public string NatinalCode { get; set; }
        [Required(ErrorMessage = "لطفا کد امنیتی را وارد نمائید")]
        [Display(Name = "کد امنیتی (به عدد)")]
        public string CaptchaInputText { get; set; }
    }
}