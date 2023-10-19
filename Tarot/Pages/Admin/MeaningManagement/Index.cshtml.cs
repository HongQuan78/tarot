using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using X.PagedList;

namespace Tarot.Pages.Admin.MeaningManagement
{
    public class IndexModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;
        public IndexModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        public IList<Meaning> Meaning { get; set; } = default!;

        // Pagination properties
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;  // Number of records per page
        public int TotalRecords { get; set; } = 0;

        public async Task OnGetAsync(int currentPage = 1)
        {
            CurrentPage = currentPage;
            TotalRecords = await _context.Meanings.CountAsync();

            Meaning = await _context.Meanings
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();
        }
    }
}
