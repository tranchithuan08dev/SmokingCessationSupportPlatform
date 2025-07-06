using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class UserAchievement
{
    public int UserAchievementId { get; set; }

    public int UserId { get; set; }

    public int AchievementId { get; set; }

    public DateTime? DateAchieved { get; set; }

    public virtual Achievement Achievement { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
