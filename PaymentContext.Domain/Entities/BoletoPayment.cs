using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public string BarCode { get; set; } 
        public string BoletoNumber { get; set; }
    }
}
