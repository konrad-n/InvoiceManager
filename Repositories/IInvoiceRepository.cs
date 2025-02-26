using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceManager.Models;

namespace InvoiceManager.Repositories
{
    public interface IInvoiceRepository : IRepositoryBase<Invoice>
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<IEnumerable<Invoice>> GetInvoicesByOwnerIdAsync(string ownerId);
        Task<IEnumerable<Invoice>> GetInvoicesByAccountantIdAsync(string accountantId);
        Task<IEnumerable<Invoice>> GetInvoicesByDateAsync(DateTime date);
        Task<Invoice> GetInvoiceByIdAsync(int invoiceId);
    }
}
