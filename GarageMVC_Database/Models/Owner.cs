  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GarageMVC_Database.Models
{
    public class Owner
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string PersonalNumber { get; set; }
        [Required]
        public string OwnerName { get; set; }
        public virtual ICollection<Vehicle> Vechicle { get; set; }
    }
}