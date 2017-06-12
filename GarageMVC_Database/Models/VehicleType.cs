using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarageMVC_Database.Models
{
    public class VehicleType
    {
        [Key]
        public int ID { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }

    public enum Category
    {
        Car,
        MC,
        Bus,
        Truck
    }
}