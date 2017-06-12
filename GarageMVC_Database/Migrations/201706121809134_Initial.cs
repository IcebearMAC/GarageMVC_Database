namespace GarageMVC_Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkingPrices", "VehicleTypeID", "dbo.VehicleTypes");
            DropIndex("dbo.ParkingPrices", new[] { "VehicleTypeID" });
            AddColumn("dbo.Vehicles", "ParkingPriceID", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "ParkingPriceID");
            AddForeignKey("dbo.Vehicles", "ParkingPriceID", "dbo.ParkingPrices", "ID", cascadeDelete: true);
            DropColumn("dbo.ParkingPrices", "VehicleTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkingPrices", "VehicleTypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Vehicles", "ParkingPriceID", "dbo.ParkingPrices");
            DropIndex("dbo.Vehicles", new[] { "ParkingPriceID" });
            DropColumn("dbo.Vehicles", "ParkingPriceID");
            CreateIndex("dbo.ParkingPrices", "VehicleTypeID");
            AddForeignKey("dbo.ParkingPrices", "VehicleTypeID", "dbo.VehicleTypes", "ID", cascadeDelete: true);
        }
    }
}
