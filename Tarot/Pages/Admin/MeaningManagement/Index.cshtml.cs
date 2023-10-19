using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using Tarot.Service;
using X.PagedList;

namespace Tarot.Pages.Admin.MeaningManagement
{
    public class IndexModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;
        public int? currentUserId { get; set; }
        private AccountService _accountService;
        public IndexModel(Tarot.Data.TarotOnlineContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public IList<Meaning> Meaning { get; set; } = default!;

        // Pagination properties
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;  // Number of records per page
        public int TotalRecords { get; set; } = 0;

        public async Task<ActionResult> OnGetAsync(int currentPage = 1)
        {
            currentUserId = HttpContext.Session.GetInt32("userId");
            if (currentUserId == null)
            {
                return Redirect("/Index");
            }

            if (_accountService.getRole(currentUserId) != "admin")
            {
                return Redirect("/Index");
            }
            CurrentPage = currentPage;
            TotalRecords = await _context.Meanings.CountAsync();

            Meaning = await _context.Meanings
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();
            return Page();
        }
    }
}
