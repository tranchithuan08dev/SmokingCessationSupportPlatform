using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects;

public partial class QuitProgress
{
    public int ProgressId { get; set; }

    public int UserId { get; set; }

    public DateTime? ReportDate { get; set; }

    public int? CigarettesSmoked { get; set; }

    public string? HealthStatus { get; set; }

    public decimal? MoneySaved { get; set; }

    public int? DaysSmokingFree { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;
}
