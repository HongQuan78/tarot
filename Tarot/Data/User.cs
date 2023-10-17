using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public string? Gender { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Role { get; set; }

    public virtual Reader? Reader { get; set; }

    public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();
}
