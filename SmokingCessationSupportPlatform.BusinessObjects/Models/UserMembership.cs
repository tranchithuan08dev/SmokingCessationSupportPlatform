using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class UserMembership
{
    public int UserMembershipId { get; set; }

    public int UserId { get; set; }

    public int PlanId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual MembershipPlan Plan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
