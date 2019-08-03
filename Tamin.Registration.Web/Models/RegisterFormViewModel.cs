using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Registration.Web.Models
{
    public class RegisterFormViewModel
    {
        [MaxLength(20, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام کاربری (انگلیسی)")]
        public string Username { get; set; }
        [MaxLength(20, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "کلمه عبور")]

        public string Password { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [EmailAddress]
        [Display(Name = "پست الکترونیک")]

        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام (انگلیسی)")]

        public string EName { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام خانوادگی (انگلیسی)")]

        public string EFamily { get; set; }



        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "کشور")]
        public string Country { get; set; }


        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "تصویر کارت ملی")]
        public string NatinalCardPhoto { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Display(Name = "تصویر آخرین مدرک تحصیلی")]

        public string LastDegrePhotoFilename { get; set; }


        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Display(Name = "موسسه/ آموزشگاه/ محل کار")]
        public string University { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Display(Name = "رشته تحصیلی")]
        public string Major { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Display(Name = "شغل")]
        public string Job { get; set; }

        [MaxLength(12, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Display(Name = "تلفن محل کار")]
        public string JobTelephone { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "صندوق پستی")]
        public string PostCode { get; set; }

        [Display(Name = "شماره پیگیری فیش واریزی")]
        public string Peygiri { get; set; }

        [Display(Name = "تصویر فیش واریزی")]
        public string PeygiriPhotoFilename { get; set; }


        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام (فارسی)")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Display(Name = "نام خانوادگی (فارسی)")]
        public string Family { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
                [Display(Name = "نام پدر")]
        public string FatherName { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
                [Display(Name = "کدملی")]
        public string NatinalCode { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
                [Display(Name = "شماره شناسنامه")]
        public string ShenasnameCode { get; set; }

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
        
        [Display(Name = "تلفن")]
        public string Telephone { get; set; }

        [MaxLength(15, ErrorMessage = "حداکثر {1} حرف وارد کنید!")]
                [Display(Name = "تلفن منزل")]
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