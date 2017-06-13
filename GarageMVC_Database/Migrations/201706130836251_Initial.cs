namespace GarageMVC_Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleOwners", "Vehicle_ID", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleOwners", "Owner_ID", "dbo.Owners");
            DropForeignKey("dbo.Vehicles", "ParkingPriceID", "dbo.ParkingPrices");
            DropForeignKey("dbo.VehicleTypes", "ParkingPrice_ID", "dbo.ParkingPrices");
            DropIndex("dbo.Vehicles", new[] { "ParkingPriceID" });
            DropIndex("dbo.VehicleTypes", new[] { "ParkingPrice_ID" });
            DropIndex("dbo.VehicleOwners", new[] { "Vehicle_ID" });
            DropIndex("dbo.VehicleOwners", new[] { "Owner_ID" });
            RenameColumn(table: "dbo.VehicleTypes", name: "ParkingPrice_ID", newName: "ParkingPriceID");
            AddColumn("dbo.Vehicles", "OwnerID", c => c.Int(nullable: false));
            AlterColumn("dbo.VehicleTypes", "ParkingPriceID", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "OwnerID");
            CreateIndex("dbo.VehicleTypes", "ParkingPriceID");
            AddForeignKey("dbo.Vehicles", "OwnerID", "dbo.Owners", "ID", cascadeDelete: true);
            AddForeignKey("dbo.VehicleTypes", "ParkingPriceID", "dbo.ParkingPrices", "ID", cascadeDelete: true);
            DropColumn("dbo.Vehicles", "ParkingPriceID");
            DropTable("dbo.VehicleOwners");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VehicleOwners",
                c => new
                    {
                        Vehicle_ID = c.Int(nullable: false),
                        Owner_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vehicle_ID, t.Owner_ID });
            
            AddColumn("dbo.Vehicles", "ParkingPriceID", c => c.Int(nullable: false));
            DropForeignKey("dbo.VehicleTypes", "ParkingPriceID", "dbo.ParkingPrices");
            DropForeignKey("dbo.Vehicles", "OwnerID", "dbo.Owners");
            DropIndex("dbo.VehicleTypes", new[] { "ParkingPriceID" });
            DropIndex("dbo.Vehicles", new[] { "OwnerID" });
            AlterColumn("dbo.VehicleTypes", "ParkingPriceID", c => c.Int());
            DropColumn("dbo.Vehicles", "OwnerID");
            RenameColumn(table: "dbo.VehicleTypes", name: "ParkingPriceID", newName: "ParkingPrice_ID");
            CreateIndex("dbo.VehicleOwners", "Owner_ID");
            CreateIndex("dbo.VehicleOwners", "Vehicle_ID");
            CreateIndex("dbo.VehicleTypes", "ParkingPrice_ID");
            CreateIndex("dbo.Vehicles", "ParkingPriceID");
            AddForeignKey("dbo.VehicleTypes", "ParkingPrice_ID", "dbo.ParkingPrices", "ID");
            AddForeignKey("dbo.Vehicles", "ParkingPriceID", "dbo.ParkingPrices", "ID", cascadeDelete: true);
            AddForeignKey("dbo.VehicleOwners", "Owner_ID", "dbo.Owners", "ID", cascadeDelete: true);
            AddForeignKey("dbo.VehicleOwners", "Vehicle_ID", "dbo.Vehicles", "ID", cascadeDelete: true);
        }
    }
}
