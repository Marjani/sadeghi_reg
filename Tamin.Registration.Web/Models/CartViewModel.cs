using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tamin.Registration.Web.Models
{
    public class CartViewModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhotoFilename { get; set; }
        public string NatinalCode { get; set; }
        public string RegisterOn { get; set; }
        public string SeatNumber { get; internal set; }
    }
}