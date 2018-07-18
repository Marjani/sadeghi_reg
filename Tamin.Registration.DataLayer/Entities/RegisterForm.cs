using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamin.Registration.DataLayer.Entities
{
    public class RegisterForm
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50 ,ErrorMessage ="حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Family { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string FatherName { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [Index(IsUnique = true)]
        public string NatinalCode { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string ShenasnameCode { get; set; }

        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public DateTime Birthday { get; set; }

        [MaxLength(20, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string City { get; set; }

        [MaxLength(1000, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Adderess { get; set; }


        [MaxLength(15, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Mobile { get; set; }

        [MaxLength(15, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Telephone { get; set; }

        [MaxLength(15, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string AltTelephone { get; set; }


        [MaxLength(500, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string PhotoFilename { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Degree { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Level { get; set; }

        [Range(12, 20, ErrorMessage = "معدل بین 12 تا 20 قابل قبول است!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public double Average { get; set; }

        //male: false
        public bool Gender { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        public string SeatNumber { get; set; }
        public DateTime RegisterOn { get; set; }


        public int Total { get; set; }
        public bool IsPaied { get; set; }
        public DateTime? PaiedOn { get; set; }
        public string TraceNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionReferenceID { get; set; }
        public string TransactionDate { get; set; }

    }
}
