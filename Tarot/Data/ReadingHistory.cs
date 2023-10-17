using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class ReadingHistory
{
    public int ReadingId { get; set; }

    public int? UserId { get; set; }

    public int? ReaderId { get; set; }

    public int? HourId { get; set; }

    public string? Notes { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public virtual WorkingHour? Hour { get; set; }

    public virtual Reader? Reader { get; set; }

    public virtual User? User { get; set; }
}
