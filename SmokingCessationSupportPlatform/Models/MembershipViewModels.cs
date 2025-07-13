using System.ComponentModel.DataAnnotations;

namespace SmokingCessationSupportPlatform.Models;

public class MembershipPlanViewModel
{
    public int PlanId { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationDays { get; set; }
    public bool IsSelected { get; set; }
}

public class PaymentViewModel
{
    [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số thẻ")]
    public string CardNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên chủ thẻ")]
    public string CardholderName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập ngày hết hạn")]
    [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Định dạng MM/YY")]
    public string ExpiryDate { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập CVV")]
    [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV phải có 3-4 chữ số")]
    public string CVV { get; set; } = string.Empty;

    public int PlanId { get; set; }
    public decimal Amount { get; set; }
}

public class MembershipStatusViewModel
{
    public bool IsActiveMember { get; set; }
    public string? PlanName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? PaymentStatus { get; set; }
    public int DaysRemaining { get; set; }
} 