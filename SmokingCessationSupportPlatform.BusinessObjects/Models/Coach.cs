using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class Coach
{
    public int CoachId { get; set; }

    public string? Specialization { get; set; }

    public string? Bio { get; set; }

    public int? ExperienceYears { get; set; }

    public virtual User CoachNavigation { get; set; } = null!;

    public virtual ICollection<CoachingSession> CoachingSessions { get; set; } = new List<CoachingSession>();
}
