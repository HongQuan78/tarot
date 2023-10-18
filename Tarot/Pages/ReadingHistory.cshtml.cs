using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages
{
    public class ReadingHistoryModel : PageModel
    {
        private readonly ReadingService _readingService;
        public List<ReadingHistory> readingHistories = new List<ReadingHistory>();
        
        public ReadingHistoryModel(ReadingService readingService)
        {
            _readingService = readingService;
        }

        public IActionResult OnGet()
        {   
           int? userId = HttpContext.Session.GetInt32("userId");
           if(userId == null)
            {
                return RedirectToPage("/login");
            }
            readingHistories = _readingService.getReadingHistoryForUser(userId);
            return Page();
        }
    }
}
