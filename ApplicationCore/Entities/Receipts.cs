using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Receipts
    {
        public string retailer { get; set; }
        public DateTime purchaseDate { get; set; }
        public TimeSpan purchaseTime { get; set; }

        public IEnumerable<Items> items { get; set; }
    }
}
