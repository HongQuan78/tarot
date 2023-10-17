using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class Reader
{
    public int ReaderId { get; set; }

    public string? Schedule { get; set; }

    public decimal? Price { get; set; }

    public decimal? Rating { get; set; }

    public string? MeetingLink { get; set; }

    public virtual User ReaderNavigation { get; set; } = null!;

    public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();

    public virtual ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
}
