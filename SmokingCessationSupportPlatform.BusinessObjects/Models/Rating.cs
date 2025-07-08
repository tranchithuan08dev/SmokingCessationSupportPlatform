using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int TargetEntityId { get; set; }

    public string TargetEntityType { get; set; } = null!;

    public int UserId { get; set; }

    public int RatingValue { get; set; }

    public string? Comment { get; set; }

    public DateTime? RatingDate { get; set; }

    public virtual User User { get; set; } = null!;
}
