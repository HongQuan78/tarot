using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Service
{
    public class ReadingService
    {
        private TarotOnlineContext _tarotOnlineContext;

        public ReadingService()
        {
            _tarotOnlineContext = new TarotOnlineContext();
        }

        public List<ReadingHistory> getReadingHistoryForUser(int? userId)
        {
            return _tarotOnlineContext.ReadingHistories.Include(o => o.Hour).Where(x => x.UserId == userId).ToList();
        }

        public List<ReadingHistory> getReadingHistoryForReaderDone(int? readerId)
        {
            return _tarotOnlineContext.ReadingHistories.Include(o => o.Hour).Where(x => x.ReaderId == readerId && x.Status == "Done").ToList();
        }

        public List<ReadingHistory> getReadingHistoryForReaderPending(int? readerId)
        {
            return _tarotOnlineContext.ReadingHistories.Include(o => o.Hour).Where(x => x.ReaderId == readerId && x.Status == "Pending").ToList();
        }

        public int countReadingDone(int? readerId)
        {
            return _tarotOnlineContext.ReadingHistories.Where(x => x.ReaderId == readerId && x.Status == "Done").Count();
        }
    }
}
