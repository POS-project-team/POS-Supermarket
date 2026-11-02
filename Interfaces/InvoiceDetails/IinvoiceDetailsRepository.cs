
using POS_Supermarket.Models;

namespace POS_Supermarket.Interfaces
{
    public interface IinvoiceDetailsRepository
    {
        public Task<IEnumerable<InvoiceDetails>> GetAllRecordesForOneInvoice(int InvoiceId);
        public Task<bool> InsertAllRecordesForOneInvoice(int InvoiceId,List<InvoiceDetails> recordes);
    }
}
