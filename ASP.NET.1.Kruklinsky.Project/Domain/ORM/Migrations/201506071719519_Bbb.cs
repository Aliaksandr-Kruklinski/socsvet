namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bbb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "Description");
        }
    }
}
