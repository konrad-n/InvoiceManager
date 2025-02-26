using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceManager.Data;
using InvoiceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Repositories
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await FindAllAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByOwnerIdAsync(string ownerId)
        {
            return await FindByConditionAsync(i => i.OwnerId == ownerId);
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByAccountantIdAsync(string accountantId)
        {
            return await FindByConditionAsync(i => i.AccountantId == accountantId);
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByDateAsync(DateTime date)
        {
            return await FindByConditionAsync(i => i.IssueDate.Date == date.Date);
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            return await DbContext.Invoice
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);
        }
    }
}
