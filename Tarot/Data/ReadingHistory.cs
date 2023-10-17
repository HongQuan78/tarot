﻿using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class ReadingHistory
{
    public int ReadingId { get; set; }

    public int? UserId { get; set; }

    public int? ReaderId { get; set; }

    public DateTime? DateTime { get; set; }

    public int? DeckId { get; set; }

    public string? Notes { get; set; }

    public virtual Deck? Deck { get; set; }

    public virtual Reader? Reader { get; set; }

    public virtual User? User { get; set; }
}