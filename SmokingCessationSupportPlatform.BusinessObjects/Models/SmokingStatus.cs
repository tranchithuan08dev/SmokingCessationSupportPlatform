using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class SmokingStatus
{
    public int StatusId { get; set; }

    public int UserId { get; set; }

    public DateOnly? ReportDate { get; set; }

    public int? CigarettesPerDay { get; set; }

    public string? Frequency { get; set; }

    public decimal? CigaretteCostPerPack { get; set; }

    public decimal? PacksPerWeek { get; set; }

    public virtual User User { get; set; } = null!;
}
