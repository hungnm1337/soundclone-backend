using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }

        public int ServiceId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
