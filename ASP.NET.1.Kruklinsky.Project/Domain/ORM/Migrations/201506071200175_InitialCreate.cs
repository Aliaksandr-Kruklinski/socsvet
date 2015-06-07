namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Topic = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Example = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Fakes",
                c => new
                    {
                        FakeId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FakeId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Topic = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        DialogId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DialogId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        MessageText = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        User_UserId = c.Guid(),
                        DialogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Dialogs", t => t.DialogId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.DialogId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        PrivateWall_WallId = c.Int(),
                        Wall_WallId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Walls", t => t.PrivateWall_WallId)
                .ForeignKey("dbo.Walls", t => t.Wall_WallId)
                .Index(t => t.PrivateWall_WallId)
                .Index(t => t.Wall_WallId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(nullable: false),
                        ImageMimeType = c.String(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Walls",
                c => new
                    {
                        WallId = c.Int(nullable: false, identity: true),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.WallId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.WallMessages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        MessageText = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false),
                        WallId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Walls", t => t.WallId, cascadeDelete: true)
                .Index(t => t.WallId);
            
            CreateTable(
                "dbo.WallComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false),
                        MessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.WallMessages", t => t.MessageId, cascadeDelete: true)
                .Index(t => t.MessageId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        Birthday = c.DateTime(),
                        AvatarId = c.Int(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.Images", t => t.AvatarId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.AvatarId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Start = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TestId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserAnswers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        ResultId = c.Int(nullable: false),
                        IsRight = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Results", t => t.ResultId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.ResultId);
            
            CreateTable(
                "dbo.QuestionsInTests",
                c => new
                    {
                        TestId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestId, t.QuestionId })
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.TestId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.UsersInDialogs",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        DialogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DialogId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Dialogs", t => t.DialogId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DialogId);
            
            CreateTable(
                "dbo.UsersInRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "UserId", "dbo.Users");
            DropForeignKey("dbo.Results", "TestId", "dbo.Tests");
            DropForeignKey("dbo.UserAnswers", "ResultId", "dbo.Results");
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Messages", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.Messages", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Wall_WallId", "dbo.Walls");
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Profiles", "AvatarId", "dbo.Images");
            DropForeignKey("dbo.Users", "PrivateWall_WallId", "dbo.Walls");
            DropForeignKey("dbo.Walls", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.WallMessages", "WallId", "dbo.Walls");
            DropForeignKey("dbo.WallComments", "MessageId", "dbo.WallMessages");
            DropForeignKey("dbo.Images", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersInDialogs", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.UsersInDialogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Tests", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.QuestionsInTests", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionsInTests", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Fakes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.UsersInDialogs", new[] { "DialogId" });
            DropIndex("dbo.UsersInDialogs", new[] { "UserId" });
            DropIndex("dbo.QuestionsInTests", new[] { "QuestionId" });
            DropIndex("dbo.QuestionsInTests", new[] { "TestId" });
            DropIndex("dbo.UserAnswers", new[] { "ResultId" });
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            DropIndex("dbo.Results", new[] { "UserId" });
            DropIndex("dbo.Results", new[] { "TestId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Profiles", new[] { "AvatarId" });
            DropIndex("dbo.WallComments", new[] { "MessageId" });
            DropIndex("dbo.WallMessages", new[] { "WallId" });
            DropIndex("dbo.Walls", new[] { "User_UserId" });
            DropIndex("dbo.Images", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Wall_WallId" });
            DropIndex("dbo.Users", new[] { "PrivateWall_WallId" });
            DropIndex("dbo.Messages", new[] { "DialogId" });
            DropIndex("dbo.Messages", new[] { "User_UserId" });
            DropIndex("dbo.Tests", new[] { "SubjectId" });
            DropIndex("dbo.Fakes", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "SubjectId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.UsersInRoles");
            DropTable("dbo.UsersInDialogs");
            DropTable("dbo.QuestionsInTests");
            DropTable("dbo.UserAnswers");
            DropTable("dbo.Results");
            DropTable("dbo.Roles");
            DropTable("dbo.Profiles");
            DropTable("dbo.WallComments");
            DropTable("dbo.WallMessages");
            DropTable("dbo.Walls");
            DropTable("dbo.Images");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Dialogs");
            DropTable("dbo.Tests");
            DropTable("dbo.Subjects");
            DropTable("dbo.Fakes");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
