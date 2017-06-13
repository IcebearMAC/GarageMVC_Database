using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarageMVC_Database.Models
{
    public class ParkingSpot
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ParkingPlace { get; set; }
        //public virtual ICollection<Vehicle> Vehicles { get; set; }
        //[ForeignKey("Vehicle")]
        //public int VehicleID { get; set; }
        //public virtual Vehicle Vehicle { get; set; }
    }
}