    using System;
    using System.Collections.Generic;

    namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

    public partial class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? FullName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string? UserRole { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        public virtual Coach? Coach { get; set; }

        public virtual ICollection<CoachingSession> CoachingSessions { get; set; } = new List<CoachingSession>();

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public virtual ICollection<QuitPlan> QuitPlans { get; set; } = new List<QuitPlan>();

        public virtual ICollection<QuitProgress> QuitProgresses { get; set; } = new List<QuitProgress>();

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        public virtual ICollection<SmokingStatus> SmokingStatuses { get; set; } = new List<SmokingStatus>();

        public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();

        public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
        public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; }= new List<PasswordResetToken>();

        public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();


    }
