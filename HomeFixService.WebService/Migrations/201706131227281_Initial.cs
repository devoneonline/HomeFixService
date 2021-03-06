namespace HomeFixService.WebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusySchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BusyPeriodStartOn = c.DateTime(nullable: false),
                        BusyPeriodEndsOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFirstName = c.String(nullable: false),
                        UserLastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StreetName = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Country = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TheProfession = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FeedbackDateTime = c.DateTime(nullable: false),
                        FeedbackPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TimeSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StartDay = c.Int(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndDay = c.Int(nullable: false),
                        EndTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 128),
                        PasswordHash = c.String(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UniqueUserNameIndex");
            
            CreateTable(
                "dbo.UserPasswordsHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CredentialsId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                        ExpiredOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.CredentialsId, t.UserId })
                .ForeignKey("dbo.Credentials", t => new { t.CredentialsId, t.UserId }, cascadeDelete: true)
                .Index(t => new { t.CredentialsId, t.UserId });
            
            CreateTable(
                "dbo.ProfessionServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserProfessionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ServiceName = c.String(nullable: false),
                        ServiceUnit = c.String(nullable: false),
                        ServiceUnitPrice = c.Single(nullable: false),
                        CurrencyUsed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserProfessionId, t.UserId })
                .ForeignKey("dbo.UserProfessions", t => new { t.UserProfessionId, t.UserId }, cascadeDelete: true)
                .Index(t => new { t.UserProfessionId, t.UserId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfessionServices", new[] { "UserProfessionId", "UserId" }, "dbo.UserProfessions");
            DropForeignKey("dbo.Credentials", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserPasswordsHistories", new[] { "CredentialsId", "UserId" }, "dbo.Credentials");
            DropForeignKey("dbo.TimeSchedules", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProfessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.BusySchedules", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropIndex("dbo.ProfessionServices", new[] { "UserProfessionId", "UserId" });
            DropIndex("dbo.UserPasswordsHistories", new[] { "CredentialsId", "UserId" });
            DropIndex("dbo.Credentials", "UniqueUserNameIndex");
            DropIndex("dbo.Credentials", new[] { "UserId" });
            DropIndex("dbo.TimeSchedules", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.UserProfessions", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropIndex("dbo.BusySchedules", new[] { "UserId" });
            DropTable("dbo.ProfessionServices");
            DropTable("dbo.UserPasswordsHistories");
            DropTable("dbo.Credentials");
            DropTable("dbo.TimeSchedules");
            DropTable("dbo.Ratings");
            DropTable("dbo.UserProfessions");
            DropTable("dbo.Contacts");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.Users");
            DropTable("dbo.BusySchedules");
        }
    }
}
