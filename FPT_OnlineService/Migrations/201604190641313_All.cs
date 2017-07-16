namespace FPT_OnlineService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class All : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseRegForms",
                c => new
                    {
                        FormID = c.Int(nullable: false),
                        SubjectCode = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        CoursesAvailable = c.String(),
                        TuitionFee = c.Boolean(nullable: false),
                        IsWeekBefore = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FormID)
                .ForeignKey("dbo.Forms", t => t.FormID)
                .Index(t => t.FormID);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Date = c.DateTime(nullable: false),
                        CurrentDesk = c.String(),
                        Flow = c.String(),
                        Status = c.String(),
                        FormDetails = c.String(),
                        ApprovalBy = c.String(),
                        ApprovalDate = c.String(),
                        StudentRollNo = c.String(),
                        StudentName = c.String(),
                        StudentEmail = c.String(),
                        StudentPhone = c.String(),
                        SemesterName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Semesters", t => t.SemesterName)
                .Index(t => t.SemesterName);
            
            CreateTable(
                "dbo.DropOutForms",
                c => new
                    {
                        FormID = c.Int(nullable: false),
                        Class = c.String(nullable: false, maxLength: 10),
                        MethodPayment = c.String(nullable: false),
                        DropOutDate = c.String(nullable: false),
                        Reason = c.String(nullable: false),
                        LibraryStatus = c.String(),
                        AccountStatus = c.String(),
                        AcademicHeadEndorse = c.String(),
                    })
                .PrimaryKey(t => t.FormID)
                .ForeignKey("dbo.Forms", t => t.FormID)
                .Index(t => t.FormID);
            
            CreateTable(
                "dbo.RequestForms",
                c => new
                    {
                        FormID = c.Int(nullable: false),
                        RequestTitle = c.String(nullable: false),
                        Class = c.String(nullable: false, maxLength: 10),
                        Description = c.String(nullable: false, maxLength: 500),
                        ProcessedBy = c.String(),
                        ApprovalReason = c.String(),
                        ToDepartment = c.String(),
                        ReceivedBy = c.String(),
                    })
                .PrimaryKey(t => t.FormID)
                .ForeignKey("dbo.Forms", t => t.FormID)
                .Index(t => t.FormID);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Year = c.Int(nullable: false),
                        NoOfWeeks = c.Int(nullable: false),
                        NoOfMonths = c.Int(nullable: false),
                        Season = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.SuspendSemesterForms",
                c => new
                    {
                        FormID = c.Int(nullable: false),
                        TuitionFee = c.Boolean(nullable: false),
                        PreviousSemester = c.String(),
                        TwoPrevSemester = c.String(),
                        IsWeekBefore = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FormID)
                .ForeignKey("dbo.Forms", t => t.FormID)
                .Index(t => t.FormID);
            
            CreateTable(
                "dbo.SuspendSubjectForms",
                c => new
                    {
                        FormID = c.Int(nullable: false),
                        SubjectCode = c.String(nullable: false),
                        SubjectName = c.String(nullable: false),
                        NotAllSubject = c.Boolean(nullable: false),
                        IsWeekBefore = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FormID)
                .ForeignKey("dbo.Forms", t => t.FormID)
                .Index(t => t.FormID);
            
            CreateTable(
                "dbo.FormComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FormID = c.Int(nullable: false),
                        RoleID = c.String(maxLength: 128),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Forms", t => t.FormID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.FormID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 256),
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.FormComments", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.FormComments", "FormID", "dbo.Forms");
            DropForeignKey("dbo.CourseRegForms", "FormID", "dbo.Forms");
            DropForeignKey("dbo.SuspendSubjectForms", "FormID", "dbo.Forms");
            DropForeignKey("dbo.SuspendSemesterForms", "FormID", "dbo.Forms");
            DropForeignKey("dbo.Forms", "SemesterName", "dbo.Semesters");
            DropForeignKey("dbo.RequestForms", "FormID", "dbo.Forms");
            DropForeignKey("dbo.DropOutForms", "FormID", "dbo.Forms");
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.FormComments", new[] { "RoleID" });
            DropIndex("dbo.FormComments", new[] { "FormID" });
            DropIndex("dbo.SuspendSubjectForms", new[] { "FormID" });
            DropIndex("dbo.SuspendSemesterForms", new[] { "FormID" });
            DropIndex("dbo.RequestForms", new[] { "FormID" });
            DropIndex("dbo.DropOutForms", new[] { "FormID" });
            DropIndex("dbo.Forms", new[] { "SemesterName" });
            DropIndex("dbo.CourseRegForms", new[] { "FormID" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.FormComments");
            DropTable("dbo.SuspendSubjectForms");
            DropTable("dbo.SuspendSemesterForms");
            DropTable("dbo.Semesters");
            DropTable("dbo.RequestForms");
            DropTable("dbo.DropOutForms");
            DropTable("dbo.Forms");
            DropTable("dbo.CourseRegForms");
        }
    }
}
