using System.ComponentModel.DataAnnotations;

namespace SmokingCessationSupportPlatform.Models;

public class SmokingStatusInputViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập số điếu thuốc mỗi ngày")]
    [Range(0, 100, ErrorMessage = "Số điếu thuốc phải từ 0 đến 100")]
    public int? CigarettesPerDay { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tần suất hút thuốc")]
    [StringLength(50)]
    public string? Frequency { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập giá tiền một bao thuốc (VNĐ)")]
    [Range(0, 1000000, ErrorMessage = "Giá tiền phải hợp lệ")]
    public decimal? CigaretteCostPerPack { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số bao mỗi tuần")]
    [Range(0, 100, ErrorMessage = "Số bao phải từ 0 đến 100")]
    public decimal? PacksPerWeek { get; set; }

    public DateOnly? ReportDate { get; set; }
}

public class SmokingStatusDisplayViewModel
{
    public int StatusId { get; set; }
    public DateOnly? ReportDate { get; set; }
    public int? CigarettesPerDay { get; set; }
    public string? Frequency { get; set; }
    public decimal? CigaretteCostPerPack { get; set; }
    public decimal? PacksPerWeek { get; set; }
} 