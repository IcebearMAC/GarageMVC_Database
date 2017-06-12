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
        public string RegistrationNumber { get; set; }
        public virtual ICollection<Owner> Owners { get; set; }
        [ForeignKey("ParkingSpot")]
        public int ParkingSpotID { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
        [ForeignKey("VehicleType")]
        public int VehicleTypeID { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}