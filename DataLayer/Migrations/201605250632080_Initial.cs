namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "TVApi.AppUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        userName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("TVApi.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "TVApi.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "TVApi.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("TVApi.User", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "TVApi.UserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("TVApi.User", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "TVApi.UserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("TVApi.User", t => t.ApplicationUser_Id)
                .ForeignKey("TVApi.Role", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "TVApi.Client",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "TVApi.FavouriteMovie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("TVApi.AppUser", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("TVApi.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "TVApi.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "TVApi.UserFriend",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendAppUserId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("TVApi.AppUser", t => t.FriendAppUserId, cascadeDelete: true)
                .Index(t => t.FriendAppUserId);
            
            CreateTable(
                "TVApi.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("TVApi.UserRole", "IdentityRole_Id", "TVApi.Role");
            DropForeignKey("TVApi.UserFriend", "FriendAppUserId", "TVApi.AppUser");
            DropForeignKey("TVApi.FavouriteMovie", "MovieId", "TVApi.Movie");
            DropForeignKey("TVApi.FavouriteMovie", "AppUserId", "TVApi.AppUser");
            DropForeignKey("TVApi.AppUser", "UserId", "TVApi.User");
            DropForeignKey("TVApi.UserRole", "ApplicationUser_Id", "TVApi.User");
            DropForeignKey("TVApi.UserLogin", "ApplicationUser_Id", "TVApi.User");
            DropForeignKey("TVApi.UserClaim", "ApplicationUser_Id", "TVApi.User");
            DropIndex("TVApi.UserFriend", new[] { "FriendAppUserId" });
            DropIndex("TVApi.FavouriteMovie", new[] { "AppUserId" });
            DropIndex("TVApi.FavouriteMovie", new[] { "MovieId" });
            DropIndex("TVApi.UserRole", new[] { "IdentityRole_Id" });
            DropIndex("TVApi.UserRole", new[] { "ApplicationUser_Id" });
            DropIndex("TVApi.UserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("TVApi.UserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("TVApi.AppUser", new[] { "UserId" });
            DropTable("TVApi.Role");
            DropTable("TVApi.UserFriend");
            DropTable("TVApi.Movie");
            DropTable("TVApi.FavouriteMovie");
            DropTable("TVApi.Client");
            DropTable("TVApi.UserRole");
            DropTable("TVApi.UserLogin");
            DropTable("TVApi.UserClaim");
            DropTable("TVApi.User");
            DropTable("TVApi.AppUser");
        }
    }
}
