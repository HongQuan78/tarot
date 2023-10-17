using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Service
{
    public class AccountService
    {
        private TarotOnlineContext _tarotOnlineContext;

        public AccountService()
        {
            _tarotOnlineContext = new TarotOnlineContext();
        }

        public User Login(string username, string password)
        {
            User user = _tarotOnlineContext.Users.Include(o => o.Reader).SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}

