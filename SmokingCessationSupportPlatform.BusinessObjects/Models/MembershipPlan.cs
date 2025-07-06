﻿using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects;

public partial class MembershipPlan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? DurationDays { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
