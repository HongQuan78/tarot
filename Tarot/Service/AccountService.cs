using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using Tarot.Model;

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

        public bool Register(RegisterDTO userDTO)
        {
            User user = new User
            {
                Fullname = userDTO.Fullname,
                Username = userDTO.Username,
                Password = userDTO.Password,
                Email = userDTO.Email,
                Birthday = userDTO.Birthday,
                Gender = userDTO.Gender,
                Phone = userDTO.Phone,
                Role = "user"
            };
            _tarotOnlineContext.Users.Add(user);
            return _tarotOnlineContext.SaveChanges() > 0;
        }
    }
}

