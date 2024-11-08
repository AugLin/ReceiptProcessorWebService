using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReceiptProcessorWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private Dictionary<int, Receipts> receipts = new Dictionary<int, Receipts>();

        [HttpPost]
        [Route("process")]
        public IActionResult UploadReceipt(Receipts receipt, int id)
        {
            if (!receipts.ContainsKey(id))
            {
                receipts.Add(id, receipt);
                return GetReceiptPoints(id);
                //return Ok();
            } else
            {
                return BadRequest("Associated ReceiptId has been taken");
            }
        }
        [HttpGet]
        [Route("{id}/points")]
        public IActionResult GetReceiptPoints(int id)
        {
            if (!receipts.ContainsKey(id)) return BadRequest("There are no associated ReceiptId");
            
            var breakdown = new List<string>();
            var receipt = receipts[id];
            var points = 0;

            int retailerPoints = receipt.retailer.Count(char.IsLetterOrDigit);
            points += retailerPoints;
            breakdown.Add($"{retailerPoints} points - Retailer name has {retailerPoints} characters");

            if (receipt.Total % 1 == 0)
            {
                points += 50;
                breakdown.Add("50 points - total is a round dollar amount");
            }

            if (receipt.Total % 0.25m == 0)
            {
                points += 25;
                breakdown.Add("25 points - total is a multiple of 0.25");
            }

            int itemPairs = receipt.items.Count / 2;
            int itemPairPoints = itemPairs * 5;
            points += itemPairPoints;

            breakdown.Add($"{itemPairPoints} points - {receipt.items.Count} items ({itemPairs} pairs @ 5 points each)");

            foreach (var item in receipt.items)
            {
                var trimmedDescription = item.ShortDescription.Trim();
                if (trimmedDescription.Length % 3 == 0)
                {
                    int itemPoints = (int)Math.Ceiling(item.Price * 0.2m);
                    points += itemPoints;
                    breakdown.Add($"{itemPoints} points - {trimmedDescription} is {trimmedDescription.Length} characters (a multiple of 3)\n" +
                        $"item price of {item.Price} * 0.2 = {item.Price * 0.2m}, rounded up is {itemPoints} points");
                }
            }

            if (receipt.purchaseDate.Day % 2 != 0)
            {
                points += 6;
                breakdown.Add("6 points - purchase day is odd");
            }

            if (receipt.ParsedPurchaseTime >= new TimeSpan(14, 0, 0) && receipt.ParsedPurchaseTime <= new TimeSpan(16, 0, 0))
            {
                points += 10;
                breakdown.Add("10 points - time of purchase is between 2:00pm and 4:00pm");
            }

            var result = new
            {
                TotalPoints = points,
                Breakdown = breakdown
            };

            return Ok(result);
        }
    }
}
