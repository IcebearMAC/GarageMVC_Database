using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GarageMVC_Database.Models;

namespace GarageMVC_Database.DataAccess
{
    public class VehicleContext : DbContext
    {
                public VehicleContext() : base("DefaultConnection") { }

                public DbSet<ParkingPrice> ParkingPrices { get; set; }
                public DbSet<VehicleType> VehiclesTypes { get; set; }
                public DbSet<Owner> Owners { get; set; }
                public DbSet<ParkingSpot> ParkingSpots { get; set; }
                public DbSet<Vehicle> Vehicles { get; set; }


    }



}