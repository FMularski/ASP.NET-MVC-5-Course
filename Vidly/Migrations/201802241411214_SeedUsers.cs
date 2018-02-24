namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'814de59a-7540-435f-9a68-17090f9957cd', N'admin@vidly.com', 0, N'ACWlmlSwxv9SdoaIwnspnIQIMPswyrKarC5OLmzgtT4mzg07RZmNlzgSkdgMZyYxuQ==', N'3cf61264-89a1-439d-a5b5-31eee2fda864', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bc92fbfd-550b-4339-9949-f3093bfe9a03', N'guest@vidly.com', 0, N'ANzshdMTh1qZ8Loih/TqVCi7/mkVh01kKjgTpePOh7LH8NKTnWztEViDqs20KgY8wA==', N'68f8a8a3-f8ba-4331-be2f-e7c06e5b810a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1cf0d21b-b5ea-437f-8e88-dadd26a761f1', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'814de59a-7540-435f-9a68-17090f9957cd', N'1cf0d21b-b5ea-437f-8e88-dadd26a761f1')

");
        }
        
        public override void Down()
        {
        }
    }
}
