using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tamin.Registration.Web.Models
{
    public class PaymentResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string PayOn { get; set; }
        public string invoiceNumber { get; set; }
        public string transactionReferenceID { get; set; }
        public string traceNumber { get; set; }
        public string referenceNumber { get; set; }
    }
}