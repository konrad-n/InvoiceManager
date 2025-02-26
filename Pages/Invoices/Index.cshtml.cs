using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InvoiceManager.Data;
using InvoiceManager.Models;
using InvoiceManager.Repositories;

namespace InvoiceManager.Pages.Invoices
{
    public class IndexModel : DiBasePageModel
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager,
            IInvoiceRepository invoiceRepository)
            : base(context, authorizationService, userManager)
        {
            _invoiceRepository = invoiceRepository;
        }

        public IList<Invoice> Invoice { get; set; }

        public async Task OnGetAsync()
        {
            var isAuthorized = User.IsInRole(Resources.ApplicationTexts.InvoiceAdministratorsRole);
            var currentUserId = UserManager.GetUserId(User);

            IEnumerable<Invoice> invoices;
            if (isAuthorized)
            {
                invoices = await _invoiceRepository.GetAllInvoicesAsync();
            }
            else
            {
                var ownerInvoices = await _invoiceRepository.GetInvoicesByOwnerIdAsync(currentUserId);
                var accountantInvoices = await _invoiceRepository.GetInvoicesByAccountantIdAsync(currentUserId);
                invoices = ownerInvoices.Union(accountantInvoices);
            }

            foreach (var invoice in invoices)
            {
                if (string.IsNullOrEmpty(invoice.AccountantId))
                {
                    invoice.AccountantId = Resources.ApplicationTexts.NoAccountantIdAdded;
                }

                if (string.IsNullOrEmpty(invoice.CompanyName))
                {
                    invoice.CompanyName = Resources.ApplicationTexts.NoCompanyNameAdded;
                }
            }

            Invoice = invoices.ToList();
        }
    }
}
