using ApplicationCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Repositories;
using ApplicationCore.Entities;

namespace Infrastructure.Repositories
{
    public class ReceiptRepositoryAsync : IReceiptRepositoryAsync
    {
        public Task<string> GetPoints(Receipts entity)
        {
            throw new NotImplementedException();
        }

        public Task<Receipts> ProcessReceipt(Receipts entity)
        {
            throw new NotImplementedException();
        }
    }
}
