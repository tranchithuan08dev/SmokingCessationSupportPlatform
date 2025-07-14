using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmokingCessationSupportPlatform.BusinessObjects.Models;

namespace SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

public partial class SmokingCessationSupportPlatformContext : DbContext
{
    public SmokingCessationSupportPlatformContext()
    {
    }

    public SmokingCessationSupportPlatformContext(DbContextOptions<SmokingCessationSupportPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<BlogComment> BlogComments { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<CoachingSession> CoachingSessions { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<MembershipPlan> MembershipPlans { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<QuitPlan> QuitPlans { get; set; }

    public virtual DbSet<QuitPlanStage> QuitPlanStages { get; set; }

    public virtual DbSet<QuitProgress> QuitProgresses { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<SmokingStatus> SmokingStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserMembership> UserMemberships { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }


 
    string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();
        return config["ConnectionStrings:DefaultConnection"];
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.AchievementId).HasName("PK__Achievem__276330E0F6A208EB");

            entity.HasIndex(e => e.AchievementName, "UQ__Achievem__D668781EB4204BD5").IsUnique();

            entity.Property(e => e.AchievementId).HasColumnName("AchievementID");
            entity.Property(e => e.AchievementName).HasMaxLength(100);
            entity.Property(e => e.Criteria).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IconUrl)
                .HasMaxLength(255)
                .HasColumnName("IconURL");
        });

        modelBuilder.Entity<BlogComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__BlogComm__C3B4DFAACDCFC5E0");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CommentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.BlogComments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogComme__PostI__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.BlogComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogComme__UserI__5629CD9C");
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogPost__AA12603815E8BAD2");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.IsPublished).HasDefaultValue(true);
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PostDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogPosts__UserI__5165187F");
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.CoachId).HasName("PK__Coaches__F411D9A16BF36547");

            entity.Property(e => e.CoachId)
                .ValueGeneratedNever()
                .HasColumnName("CoachID");
            entity.Property(e => e.Specialization).HasMaxLength(100);

            entity.HasOne(d => d.CoachNavigation).WithOne(p => p.Coach)
                .HasForeignKey<Coach>(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coaches__CoachID__59063A47");
        });

        modelBuilder.Entity<CoachingSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Coaching__C9F492703670531E");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.SessionDateTime).HasColumnType("datetime");
            entity.Property(e => e.SessionStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Scheduled");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachingSessions)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoachingS__Coach__5DCAEF64");

            entity.HasOne(d => d.User).WithMany(p => p.CoachingSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoachingS__UserI__5CD6CB2B");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF652645B30");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FeedbackType).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("New");
            entity.Property(e => e.Subject).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__6754599E");
        });

        modelBuilder.Entity<MembershipPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Membersh__755C22D773C7B765");

            entity.HasIndex(e => e.PlanName, "UQ__Membersh__46E12F9E5CC49786").IsUnique();

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PlanName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32535613C9");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.NotificationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NotificationType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__6C190EBB");
        });

        modelBuilder.Entity<QuitPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__QuitPlan__755C22D7975B45EC");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.CurrentStage).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PlanName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.QuitPlans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuitPlans__UserI__3A81B327");
        });

        modelBuilder.Entity<QuitPlanStage>(entity =>
        {
            entity.HasKey(e => e.StageId).HasName("PK__QuitPlan__03EB7AF856A87BE0");

            entity.Property(e => e.StageId).HasColumnName("StageID");
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.StageName).HasMaxLength(100);

            entity.HasOne(d => d.Plan).WithMany(p => p.QuitPlanStages)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuitPlanS__PlanI__3E52440B");
        });

        modelBuilder.Entity<QuitProgress>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__QuitProg__BAE29C85CB22DA6C");

            entity.ToTable("QuitProgress");

            entity.Property(e => e.ProgressId).HasColumnName("ProgressID");
            entity.Property(e => e.CigarettesSmoked).HasDefaultValue(0);
            entity.Property(e => e.DaysSmokingFree).HasDefaultValue(0);
            entity.Property(e => e.MoneySaved)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.QuitProgresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuitProgr__UserI__44FF419A");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF85CF6DC9C49");

            entity.Property(e => e.RatingId).HasColumnName("RatingID");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.RatingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TargetEntityId).HasColumnName("TargetEntityID");
            entity.Property(e => e.TargetEntityType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ratings__UserID__628FA481");
        });

        modelBuilder.Entity<SmokingStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__SmokingS__C8EE20430EEF8DF0");

            entity.ToTable("SmokingStatus");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.CigaretteCostPerPack).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.PacksPerWeek).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReportDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.SmokingStatuses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SmokingSt__UserI__36B12243");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC16823A08");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E47CD42352").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A1B98E62").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserRole)
                .HasMaxLength(20)
                .HasDefaultValue("Member");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => e.UserAchievementId).HasName("PK__UserAchi__07E627D6FE4EAA92");

            entity.Property(e => e.UserAchievementId).HasColumnName("UserAchievementID");
            entity.Property(e => e.AchievementId).HasColumnName("AchievementID");
            entity.Property(e => e.DateAchieved)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Achievement).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.AchievementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAchie__Achie__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAchie__UserI__4BAC3F29");
        });

        modelBuilder.Entity<UserMembership>(entity =>
        {
            entity.HasKey(e => e.UserMembershipId).HasName("PK__UserMemb__5A4E730A2617ACE7");

            entity.Property(e => e.UserMembershipId).HasColumnName("UserMembershipID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Plan).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserMembe__PlanI__32E0915F");

            entity.HasOne(d => d.User).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserMembe__UserI__31EC6D26");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Token)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(e => e.ExpiryTime)
                  .HasColumnType("datetime");
            entity.Property(e => e.IsUsed).IsRequired();
            entity.HasOne(e => e.User)
                  .WithMany(u => u.PasswordResetTokens)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
