
using POS_Supermarket.Models;

namespace POS_Supermarket.Interfaces
{
    public interface IInvoiceDetailsRepository
    {
        public Task<bool> AddListOfDetailsForOneInvoiceAsync(int InvoiceId, IEnumerable<InvoiceDetails> Recordes);
        public Task<IEnumerable<InvoiceDetails>> GetDetailsForOneInvoceAsync(int InvoiceId);
    }
}
