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
        public int ParkingPrice { get; set; }
        public virtual ICollection<VehicleType> VehicleTypes { get; set; }
        [ForeignKey("VehicleType")]
        public int VehicleTypeID { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }

}