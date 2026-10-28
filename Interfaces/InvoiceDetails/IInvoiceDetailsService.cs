
using POS_Supermarket.Models;

namespace POS_Supermarket.Interfaces
{
    public interface IInvoiceDetailsService
    {
        public Task<bool> AddListOfDetailsForOneInvoice(int? InvoiceId, IEnumerable<InvoiceDetails>? Recordes);
        public Task<IEnumerable<InvoiceDetails>> GetDetailsForOneInvoce(int? InvoiceId);
    }
}
