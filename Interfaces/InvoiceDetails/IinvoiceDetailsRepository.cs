
using POS_Supermarket.Models;

namespace POS_Supermarket.Interfaces
{
    public interface IinvoiceDetailsRepository
    {
        public Task<IEnumerable<InvoiceDetails>> GetAllRecordesForOneInvoiceAsync(int InvoiceId);
        public Task<bool> InsertAllRecordesForOneInvoiceAsync(int InvoiceId,IEnumerable<InvoiceDetails> recordes);
    }
}
