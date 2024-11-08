using ReceiptProcessorWebService.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Receipts
    {
        public string retailer { get; set; }
        public DateTime purchaseDate { get; set; }
        public string purchaseTime { get; set; }
        public List<Item> items { get; set; }
        public decimal Total { get; set; }
        public TimeSpan ParsedPurchaseTime => TimeSpan.ParseExact(purchaseTime, @"hh\:mm", CultureInfo.InvariantCulture);

    }
}
