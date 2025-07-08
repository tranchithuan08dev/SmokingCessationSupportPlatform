using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class QuitPlanStage
{
    public int StageId { get; set; }

    public int PlanId { get; set; }

    public string StageName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? TargetDate { get; set; }

    public bool? IsCompleted { get; set; }

    public virtual QuitPlan Plan { get; set; } = null!;
}
