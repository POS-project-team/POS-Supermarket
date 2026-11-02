using POS_Supermarket.Interfaces;
using POS_Supermarket.Models;


namespace POS_Supermarket.Services
{
    public class InvoiceDetailsService(IinvoiceDetailsRepository repository) : IInvoiceDetailsService
    {
        public async Task<bool> AddListOfDetailsForOneInvoice(int? InvoiceId, IEnumerable<InvoiceDetails>? Recordes)
        {
            if (Recordes is null || InvoiceId is null)
                return false;

            return await repository.InsertAllRecordesForOneInvoiceAsync(InvoiceId.Value, Recordes);
        }
        public async Task<IEnumerable<InvoiceDetails>> GetDetailsForOneInvoce(int? InvoiceId)
        {
            if (InvoiceId is null)
                return new List<InvoiceDetails>();
            return await repository.GetAllRecordesForOneInvoiceAsync(InvoiceId.Value);
        }
    }
}