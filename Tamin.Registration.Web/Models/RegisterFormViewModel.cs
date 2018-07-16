using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Registration.Web.Models
{
    public class RegisterFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "کدملی")]
        public string NatinalCode { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "شماره شناسنامه")]
        public string ShenasnameCode { get; set; }

        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "تاریخ تولد")]
        public string Birthday { get; set; }

        [MaxLength(20, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "شهر")]
        public string City { get; set; }

        [MaxLength(1000, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "آدرس")]
        public string Adderess { get; set; }


        [MaxLength(15, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [MaxLength(15, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "تلفن")]
        public string Telephone { get; set; }

        [MaxLength(15, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "تلفن ضروری")]
        public string AltTelephone { get; set; }


        [MaxLength(500, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        //[Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "عکس پرسنلی")]
        public string PhotoFilename { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "رشته تحصیلی")]
        public string Degree { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "مدرک تحصیلی")]
        public string Level { get; set; }

        [Range(0, 20, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "معدل")]
        public double Average { get; set; }

        //male: false
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "لطفا کد امنیتی را وارد نمائید")]
        [Display(Name = "کد امنیتی (به عدد)")]
        public string CaptchaInputText { get; set; }

    }
}