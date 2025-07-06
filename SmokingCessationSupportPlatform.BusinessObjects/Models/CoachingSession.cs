using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects;

public partial class CoachingSession
{
    public int SessionId { get; set; }

    public int UserId { get; set; }

    public int CoachId { get; set; }

    public DateTime SessionDateTime { get; set; }

    public int? DurationMinutes { get; set; }

    public string? SessionStatus { get; set; }

    public string? Notes { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
