﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarageMVC_Database.Models
{
    public class ParkingPrice
    {
        public int ID { get; set; }
        public int ParkingPrices { get; set; }
        //public virtual ICollection<Vehicle> Vehicles { get; set; }
        //[ForeignKey("VehicleType")]
        //public int VehicleTypeID { get; set; }
        //public virtual VehicleType VehicleType { get; set; }
    }

}   