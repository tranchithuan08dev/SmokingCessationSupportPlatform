using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new SmokingCessationSupportPlatformContext(
            serviceProvider.GetRequiredService<DbContextOptions<SmokingCessationSupportPlatformContext>>());

        // Seed Membership Plans
        if (!context.MembershipPlans.Any())
        {
            var plans = new List<MembershipPlan>
            {
                new MembershipPlan
                {
                    PlanName = "Gói Cơ Bản",
                    Description = "Gói dành cho người mới bắt đầu cai thuốc lá. Bao gồm các tính năng cơ bản hỗ trợ.",
                    Price = 199000,
                    DurationDays = 30,
                    IsActive = true
                },
                new MembershipPlan
                {
                    PlanName = "Gói Nâng Cao",
                    Description = "Gói dành cho người đã có kinh nghiệm. Bao gồm tư vấn chuyên gia và theo dõi chi tiết.",
                    Price = 399000,
                    DurationDays = 30,
                    IsActive = true
                },
                new MembershipPlan
                {
                    PlanName = "Gói Premium",
                    Description = "Gói cao cấp với đầy đủ tính năng. Bao gồm tư vấn 1-1, hỗ trợ 24/7 và các tính năng đặc biệt.",
                    Price = 699000,
                    DurationDays = 30,
                    IsActive = true
                },
                new MembershipPlan
                {
                    PlanName = "Gói 6 Tháng",
                    Description = "Gói tiết kiệm cho 6 tháng. Phù hợp cho người muốn cai thuốc lâu dài.",
                    Price = 999000,
                    DurationDays = 180,
                    IsActive = true
                },
                new MembershipPlan
                {
                    PlanName = "Gói 1 Năm",
                    Description = "Gói dài hạn với giá ưu đãi. Cam kết đồng hành cùng bạn trong hành trình cai thuốc.",
                    Price = 1599000,
                    DurationDays = 365,
                    IsActive = true
                }
            };

            context.MembershipPlans.AddRange(plans);
            context.SaveChanges();
        }
    }
} 