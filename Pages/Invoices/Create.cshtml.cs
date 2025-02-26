using System;
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
    public class CreateModel : DiBasePageModel
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager,
            IInvoiceRepository invoiceRepository)
            : base(context, authorizationService, userManager)
        {
            _invoiceRepository = invoiceRepository;
        }

        public IActionResult OnGet()
        {
            Invoice = new Invoice
            {
                IssueDate = DateTime.Today,
                DueDate = DateTime.Today
            };
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException(string.Format(Resources.ApplicationTexts.UnableToLoadUserWithId, UserManager.GetUserId(User)));
            }

            Invoice.OwnerId = UserManager.GetUserId(User);
            Invoice.AccountantId = user.AccountantId;
            Invoice.UserCompanyName = user.UserCompanyName;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Invoice, InvoiceOperations.Create);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            await _invoiceRepository.CreateAsync(Invoice);
            await _invoiceRepository.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
