using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public string? FeedbackType { get; set; }

    public string? Subject { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? FeedbackDate { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
