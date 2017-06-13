using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarageMVC_Database.Models
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(6)]
        public string RegNumber { get; set; }
        [ForeignKey("ParkingSpot")]
        public int ParkingSpotID { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
        [ForeignKey("VehicleType")]
        public int VehicleTypeID { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        [ForeignKey("Owner")]
        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }
    }
}