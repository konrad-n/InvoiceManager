using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InvoiceManager.Authorization;
using InvoiceManager.Data;
using InvoiceManager.Models;
using InvoiceManager.Repositories;

namespace InvoiceManager.Pages.Invoices
{
    public class DetailsModel : DiBasePageModel
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager,
            IInvoiceRepository invoiceRepository)
            : base(context, authorizationService, userManager)
        {
            _invoiceRepository = invoiceRepository;
        }

        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);

            if (Invoice == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(Invoice.AccountantId))
            {
                Invoice.AccountantId = Resources.ApplicationTexts.NoAccountantIdAdded;
            }

            if (string.IsNullOrEmpty(Invoice.CompanyName))
            {
                Invoice.CompanyName = Resources.ApplicationTexts.NoCompanyNameAdded;
            }

            var isAuthorized = User.IsInRole(Resources.ApplicationTexts.InvoiceAccountantRole) ||
                               User.IsInRole(Resources.ApplicationTexts.InvoiceAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized && currentUserId != Invoice.OwnerId && Invoice.Status != InvoiceStatus.Approved)
            {
                return new ChallengeResult();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, InvoiceStatus status)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            var invoiceOperation = (status == InvoiceStatus.Approved)
                ? InvoiceOperations.Approve
                : InvoiceOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, invoice, invoiceOperation);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            invoice.Status = status;
            await _invoiceRepository.UpdateAsync(invoice);
            await _invoiceRepository.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
