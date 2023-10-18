using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages
{
    public class ReaderProfileModel : PageModel
    {
        public AccountService accountService;
        public ReadingService readingService;
        [BindProperty]
        public string name {  get; set; }
        [BindProperty]
        public string phone { get; set; }
        [BindProperty]
        public string mail { get; set; }
        [BindProperty]
        public string linkMeet { get; set; }
        [BindProperty]
        public List<WorkingHour> workingHour { get; set; }
        [BindProperty]
        public decimal? rating { get; set; }
        [BindProperty]
        public int? currentUserId { get; set; }
        [BindProperty]
        public List<ReadingHistory> readingHistoriesDone { get; set; }

        [BindProperty]
        public List<ReadingHistory> readingHistoriesPending { get; set; }

        [BindProperty]
        public int numberOfCalls { get; set; }

        public ReaderProfileModel()
        {
            accountService = new AccountService();
            readingService = new ReadingService();
        }
        public IActionResult OnGet()
        {
            currentUserId = HttpContext.Session.GetInt32("userId");
            if(currentUserId == null)
            {
                return Redirect("/Index");
            }
            else
            {
                if (accountService.getRole(currentUserId) == "reader")
                {
                    User user = accountService.getUser(currentUserId, "reader");
                    if (user != null)
                    {
                        name = user.Username;
                        phone = user.Phone;
                        mail = user.Email;
                        linkMeet = user.Reader.MeetingLink;
                        workingHour = user.Reader.WorkingHours.ToList();
                        rating = user.Reader.Rating;
                        readingHistoriesDone = readingService.getReadingHistoryForReaderDone(currentUserId);
                        numberOfCalls = readingService.countReadingDone(currentUserId);
                        readingHistoriesPending = readingService.getReadingHistoryForReaderPending(currentUserId);
                    }
         
                    return Page();
                }
                else
                {
                    return Redirect("/Index");
                }
            }
        }
        
    }
}
