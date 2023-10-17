using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class WorkingHour
{
    public int HourId { get; set; }

    public int? ReaderId { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public string? DayOfWeek { get; set; }

    public virtual Reader? Reader { get; set; }

    public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();
}
