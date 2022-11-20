using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class Invoicelineitem
    {
        public int InvoiceId { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }
        public override string ToString()
        {
            return InvoiceId + ", " + ProductCode + ", " + UnitPrice + ", " + Quantity + ", " + ItemTotal;
        }
        public virtual Invoice Invoice { get; set; }
        public virtual Product ProductCodeNavigation { get; set; }
    }
}
