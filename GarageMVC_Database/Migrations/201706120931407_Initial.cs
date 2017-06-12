namespace GarageMVC_Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonalNumber = c.String(nullable: false),
                        OwnerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false),
                        ParkingSpotID = c.Int(nullable: false),
                        VehicleTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ParkingSpots", t => t.ParkingSpotID, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeID, cascadeDelete: true)
                .Index(t => t.ParkingSpotID)
                .Index(t => t.VehicleTypeID);
            
            CreateTable(
                "dbo.ParkingSpots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        ParkingPrice_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ParkingPrices", t => t.ParkingPrice_ID)
                .Index(t => t.ParkingPrice_ID);
            
            CreateTable(
                "dbo.ParkingPrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParkingPrices = c.Int(nullable: false),
                        VehicleTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeID, cascadeDelete: true)
                .Index(t => t.VehicleTypeID);
            
            CreateTable(
                "dbo.VehicleOwners",
                c => new
                    {
                        Vehicle_ID = c.Int(nullable: false),
                        Owner_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vehicle_ID, t.Owner_ID })
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_ID, cascadeDelete: true)
                .ForeignKey("dbo.Owners", t => t.Owner_ID, cascadeDelete: true)
                .Index(t => t.Vehicle_ID)
                .Index(t => t.Owner_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleTypes", "ParkingPrice_ID", "dbo.ParkingPrices");
            DropForeignKey("dbo.ParkingPrices", "VehicleTypeID", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "VehicleTypeID", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "ParkingSpotID", "dbo.ParkingSpots");
            DropForeignKey("dbo.VehicleOwners", "Owner_ID", "dbo.Owners");
            DropForeignKey("dbo.VehicleOwners", "Vehicle_ID", "dbo.Vehicles");
            DropIndex("dbo.VehicleOwners", new[] { "Owner_ID" });
            DropIndex("dbo.VehicleOwners", new[] { "Vehicle_ID" });
            DropIndex("dbo.ParkingPrices", new[] { "VehicleTypeID" });
            DropIndex("dbo.VehicleTypes", new[] { "ParkingPrice_ID" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeID" });
            DropIndex("dbo.Vehicles", new[] { "ParkingSpotID" });
            DropTable("dbo.VehicleOwners");
            DropTable("dbo.ParkingPrices");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.ParkingSpots");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Owners");
        }
    }
}
