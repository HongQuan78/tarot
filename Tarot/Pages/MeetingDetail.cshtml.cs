using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages
{
    public class MeetingDetailModel : PageModel
    {
        private readonly ReadingService _readingService;
        public ReadingHistory readingHistory;
        public MeetingDetailModel(ReadingService readingService)
        {
            _readingService = readingService;
        }
        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return Redirect("/Index");
            }

            readingHistory = _readingService.GetHistoryForUser(id);
            if (readingHistory == null)
            {
                return Redirect("/Index");
            }
            else
            {
                return Page();
            }
            return Page();
        }
    }
}
