using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages
{
    public class ReaderDetailModel : PageModel
    {
        private readonly TarotOnlineContext _context;
        public User user { get; set; }
        [BindProperty]
        public decimal? Price { get; set; }
        [BindProperty]
        public int? ReaderId { get; set; }
        [BindProperty]
        public int SelectedSlot { get; set; }
        [BindProperty]
        public List<WorkingHour> workingHour { get; set; }
        public ReaderDetailModel(TarotOnlineContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            user = _context.Users.Include(x => x.Reader).ThenInclude(a => a.WorkingHours).FirstOrDefault(x => x.UserId == id);
            Price = user.Reader.Price;
            workingHour = user.Reader.WorkingHours.Where(x => x.ReaderId == id).ToList();
            Console.WriteLine(user);
        }

        public void OnPost()
        {
            var HourId = SelectedSlot;
            var UserId = HttpContext.Session.GetInt32("userId");
            var ReaderIdInput = ReaderId;
            var readingHistory = new ReadingHistory
            {
                Notes = "",
                Status = "pending",
            };
            readingHistory.Price = Price;
            readingHistory.HourId = HourId;
            readingHistory.UserId = UserId;
            readingHistory.ReaderId = ReaderIdInput;
            _context.ReadingHistories.Add(readingHistory);
            _context.SaveChanges();
            TempData["Message"] = "Booking successful!";
        }
    }
}
