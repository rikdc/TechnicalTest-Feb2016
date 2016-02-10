namespace PostcodeSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Postcode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Postcodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Thoroughfare = c.String(),
                        Posttown = c.String(),
                        Postcode = c.String(),
                        Easting = c.Int(nullable: false),
                        Northing = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Postcodes");
        }
    }
}
