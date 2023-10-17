using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Data;

namespace Tarot.Pages
{
    public class ReaderDetailModel : PageModel
    {
        private readonly TarotOnlineContext _context;
        public Reader reader = new Reader();
        public User user = new User();
        public ReaderDetailModel(TarotOnlineContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            reader = _context.Readers.FirstOrDefault(x => x.ReaderId == id);
            user = _context.Users.FirstOrDefault(x => x.UserId == reader.ReaderId);
            Console.WriteLine(reader);
            Console.WriteLine(user);
        }
    }
}
