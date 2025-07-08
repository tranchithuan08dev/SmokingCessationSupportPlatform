using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmokingCessationSupportPlatform.DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AchievementName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Criteria = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IconURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Achievem__276330E0F6A208EB", x => x.AchievementID);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlans",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Membersh__755C22D773C7B765", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastLoginDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Member"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC16823A08", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BlogPost__AA12603815E8BAD2", x => x.PostID);
                    table.ForeignKey(
                        name: "FK__BlogPosts__UserI__5165187F",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    CoachID = table.Column<int>(type: "int", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceYears = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coaches__F411D9A16BF36547", x => x.CoachID);
                    table.ForeignKey(
                        name: "FK__Coaches__CoachID__59063A47",
                        column: x => x.CoachID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    FeedbackType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "New")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDF652645B30", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Feedback__UserID__6754599E",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    NotificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsRead = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E32535613C9", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__6C190EBB",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "QuitPlans",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReasonToQuit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TargetQuitDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CurrentStage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuitPlan__755C22D7975B45EC", x => x.PlanID);
                    table.ForeignKey(
                        name: "FK__QuitPlans__UserI__3A81B327",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "QuitProgress",
                columns: table => new
                {
                    ProgressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CigarettesSmoked = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    HealthStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoneySaved = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0m),
                    DaysSmokingFree = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuitProg__BAE29C85CB22DA6C", x => x.ProgressID);
                    table.ForeignKey(
                        name: "FK__QuitProgr__UserI__44FF419A",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetEntityID = table.Column<int>(type: "int", nullable: false),
                    TargetEntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RatingDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ratings__FCCDF85CF6DC9C49", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK__Ratings__UserID__628FA481",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "SmokingStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ReportDate = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    CigarettesPerDay = table.Column<int>(type: "int", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CigaretteCostPerPack = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PacksPerWeek = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SmokingS__C8EE20430EEF8DF0", x => x.StatusID);
                    table.ForeignKey(
                        name: "FK__SmokingSt__UserI__36B12243",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    UserAchievementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AchievementID = table.Column<int>(type: "int", nullable: false),
                    DateAchieved = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserAchi__07E627D6FE4EAA92", x => x.UserAchievementID);
                    table.ForeignKey(
                        name: "FK__UserAchie__Achie__4CA06362",
                        column: x => x.AchievementID,
                        principalTable: "Achievements",
                        principalColumn: "AchievementID");
                    table.ForeignKey(
                        name: "FK__UserAchie__UserI__4BAC3F29",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserMemberships",
                columns: table => new
                {
                    UserMembershipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserMemb__5A4E730A2617ACE7", x => x.UserMembershipID);
                    table.ForeignKey(
                        name: "FK__UserMembe__PlanI__32E0915F",
                        column: x => x.PlanID,
                        principalTable: "MembershipPlans",
                        principalColumn: "PlanID");
                    table.ForeignKey(
                        name: "FK__UserMembe__UserI__31EC6D26",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BlogComm__C3B4DFAACDCFC5E0", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK__BlogComme__PostI__5535A963",
                        column: x => x.PostID,
                        principalTable: "BlogPosts",
                        principalColumn: "PostID");
                    table.ForeignKey(
                        name: "FK__BlogComme__UserI__5629CD9C",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CoachingSessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CoachID = table.Column<int>(type: "int", nullable: false),
                    SessionDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: true),
                    SessionStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Scheduled"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coaching__C9F492703670531E", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK__CoachingS__Coach__5DCAEF64",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID");
                    table.ForeignKey(
                        name: "FK__CoachingS__UserI__5CD6CB2B",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "QuitPlanStages",
                columns: table => new
                {
                    StageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuitPlan__03EB7AF856A87BE0", x => x.StageID);
                    table.ForeignKey(
                        name: "FK__QuitPlanS__PlanI__3E52440B",
                        column: x => x.PlanID,
                        principalTable: "QuitPlans",
                        principalColumn: "PlanID");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Achievem__D668781EB4204BD5",
                table: "Achievements",
                column: "AchievementName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_PostID",
                table: "BlogComments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserID",
                table: "BlogComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_UserID",
                table: "BlogPosts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CoachingSessions_CoachID",
                table: "CoachingSessions",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_CoachingSessions_UserID",
                table: "CoachingSessions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserID",
                table: "Feedback",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Membersh__46E12F9E5CC49786",
                table: "MembershipPlans",
                column: "PlanName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserId",
                table: "PasswordResetTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuitPlans_UserID",
                table: "QuitPlans",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuitPlanStages_PlanID",
                table: "QuitPlanStages",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_QuitProgress_UserID",
                table: "QuitProgress",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserID",
                table: "Ratings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SmokingStatus_UserID",
                table: "SmokingStatus",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementID",
                table: "UserAchievements",
                column: "AchievementID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserID",
                table: "UserAchievements",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_PlanID",
                table: "UserMemberships",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_UserID",
                table: "UserMemberships",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E47CD42352",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534A1B98E62",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "CoachingSessions");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "QuitPlanStages");

            migrationBuilder.DropTable(
                name: "QuitProgress");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "SmokingStatus");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "UserMemberships");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "QuitPlans");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "MembershipPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
