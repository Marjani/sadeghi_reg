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

        [MaxLength(25, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Username { get; set; }

        [MaxLength(20, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Password { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        [EmailAddress(ErrorMessage ="پست الکترونیک صحبح وارد کنید!")]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string EName { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string EFamily { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string NatinalCardPhoto { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        public string Job { get; set; }
        public string JobTelephone { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string PostCode { get; set; }
        public string Peygiri { get; set; }
        public string PeygiriPhotoFilename { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Family { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]

        public string FatherName { get; set; }

        [MaxLength(12, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string NatinalCode { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        public string ShenasnameCode { get; set; }


        public DateTime Birthday { get; set; }

        [MaxLength(20, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string City { get; set; }

        [MaxLength(1000, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Adderess { get; set; }


        [MaxLength(15, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Mobile { get; set; }

        [MaxLength(15, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        public string Telephone { get; set; }

        [MaxLength(15, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
  //      [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string AltTelephone { get; set; }


        [MaxLength(500, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string PhotoFilename { get; set; }

        [MaxLength(10, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Degree { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]

        public string Level { get; set; }

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
        public string LastDegrePhotoFilename { get; set; }
        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string University { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
 //       [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Major { get; set; }

        [MaxLength(20, ErrorMessage = "حداکثر {0} حرف وارد کنید!")]
//        [Required(ErrorMessage = "وارد کردن مقدار {0} الزامی است!")]
        public string Country { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
