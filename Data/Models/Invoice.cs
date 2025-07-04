using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int ServiceId { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
