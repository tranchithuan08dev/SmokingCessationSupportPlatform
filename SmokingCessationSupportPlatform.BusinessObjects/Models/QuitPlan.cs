using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class QuitPlan
{
    public int PlanId { get; set; }

    public int UserId { get; set; }

    public string? PlanName { get; set; }

    public string? ReasonToQuit { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? TargetQuitDate { get; set; }

    public string? CurrentStage { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<QuitPlanStage> QuitPlanStages { get; set; } = new List<QuitPlanStage>();

    public virtual User User { get; set; } = null!;
}
