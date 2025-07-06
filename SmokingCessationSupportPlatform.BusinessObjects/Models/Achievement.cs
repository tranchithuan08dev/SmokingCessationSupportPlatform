using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects;

public partial class Achievement
{
    public int AchievementId { get; set; }

    public string AchievementName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Criteria { get; set; }

    public string? IconUrl { get; set; }

    public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
