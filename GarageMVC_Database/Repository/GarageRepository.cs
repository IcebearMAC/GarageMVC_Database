using GarageMVC_Database.DataAccess;
using GarageMVC_Database.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GarageMVC_Database.Repository
{
    public class GarageRepository
    {
        private VehicleContext db;

        #region Constructors
        public GarageRepository()
        {
            db = new VehicleContext();
        }
        #endregion

        //Add a vehicle to database
        public bool Add(GarageMVC_Database.Models.Vehicle vehicle)
        {
            bool exists = false;
            if (vehicle != null)
            {
                vehicle.RegNumber = vehicle.RegNumber.ToUpper();
                //If Vehicle Exists
                if (db.Vehicles.Where(v=>v.RegNumber==vehicle.RegNumber).FirstOrDefault()!=null)
                {
                    exists = true;
                }

                else
                {
                    bool Once = true;
                    int index = 1;

                    foreach (var v in db.ParkingSpots.OrderBy(v => v.ParkingPlace))
                    {
                        //Set the parking place for the vehicle to the empty parking slot
                        if (index != v.ParkingPlace && Once == true)
                        {
                            vehicle.ParkingSpot.ParkingPlace = index;
                            Once = false;
                            break;
                        }
                        index++;
                    }
                    //If the Vehicle doesn't exist in the database, add it to the db
                    if (exists == false)
                    {
                        if (vehicle.ParkingSpot.ParkingPlace == 0) { vehicle.ParkingSpot.ParkingPlace = index; }

                        vehicle = SetDefaultPrice(vehicle);

                        db.Entry(vehicle).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
            }
            return exists;
        }
        #region Get Vehicle(s)

        //GET all vehicles from database
        public List<Vehicle> GetAll()
        {
            return db.Vehicles.ToList();
        }
        public Vehicle GetVehicle(string regNr)
        {
            return db.Vehicles.Where(v => v.RegNumber == regNr).FirstOrDefault();
        }
        public Vehicle GetVehicle(int id)
        {
            return db.Vehicles.Where(v => v.ID == id).FirstOrDefault();
        }
        //GET filtered vehicle list
        public List<Vehicle> GetFilteredList(string type)
        {
            if (type == "Car")
            {
                return db.Vehicles.Where(vehicle => vehicle.VehicleType.Category == Category.Car).ToList();
            }
            else if (type == "Bus")
            {
                return db.Vehicles.Where(vehicle => vehicle.VehicleType.Category == Category.Bus).ToList();
            }
            else if (type == "Mc" || type == "MC")
            {
                return db.Vehicles.Where(vehicle => vehicle.VehicleType.Category == Category.Mc).ToList();
            }
            else if (type == "Truck" || type == "Truck")
            {
                return db.Vehicles.Where(vehicle => vehicle.VehicleType.Category == Category.Truck).ToList();
            }
            else
                return db.Vehicles.Where(vehicle => vehicle.VehicleType.Category == Category.Other).ToList();
        }
        //GET Sorted Lists
        public List<Vehicle> SortParking(bool descend)
        {
            if (descend)
            {
                return db.Vehicles.OrderByDescending(v => v.ParkingSpot.ParkingPlace).ToList();
            }
            return db.Vehicles.OrderBy(v => v.ParkingSpot).ToList();
        }
        //public List<Vehicle> SortOwner(bool descend)
        //{
        //    if (descend)
        //    {
        //        return db.Vehicles.OrderByDescending(v => v.Owner.FirstOrDefault().OwnerName).ToList();
        //    }
        //    return db.Vehicles.OrderBy(v => v.Owner.FirstOrDefault().OwnerName).ToList();
        //}
        public List<Vehicle> SortDate(bool descend)
        {
            if (descend)
            {
                return db.Vehicles.OrderByDescending(v => v.ParkingSpot.StartTime).ToList();
            }
            return db.Vehicles.OrderBy(v => v.ParkingSpot.StartTime).ToList();
        }
        public List<Vehicle> SortReg(bool descend)
        {
            if (descend)
            {
                return db.Vehicles.OrderByDescending(v => v.RegNumber).ToList();
            }
            return db.Vehicles.OrderBy(v => v.RegNumber).ToList();
        }
        public List<Vehicle> SortType(bool descend)
        {
            if (descend)
            {
                return db.Vehicles.OrderByDescending(v => v.VehicleType.Category).ToList();
            }
            return db.Vehicles.OrderBy(v => v.VehicleType.Category).ToList();
        }
        #endregion
        //Edit a vehicle
        public void Edit(Vehicle vehicle)
        {
            //Edits the element without removing and inserting it
            db.Entry(vehicle).State = EntityState.Modified;
            //Saves the new Data in the Database
            db.SaveChanges();
        }
        //Updates the parking prices for each vehicle
        public void UpdateParkPrice()
        {
            foreach (var v in db.Vehicles)
            {
                //reset the parkingPrice to it's default values
                Vehicle vehicle = SetDefaultPrice(v);

                //Calculate the timespan and than update the cost
                System.TimeSpan tspan = System.DateTime.Now - vehicle.ParkingSpot.StartTime;
                decimal temp=0.5M * (System.Convert.ToDecimal(tspan.TotalMinutes));
            }
            db.SaveChanges();
        }
        // Updates price for a specific vehicle
        public void UpdateVehiclePrice(int id)
        {
            Vehicle veh = db.Vehicles.Where(v => v.ID == id).FirstOrDefault();
            veh = SetDefaultPrice(veh);
            System.TimeSpan tspan = System.DateTime.Now - veh.ParkingSpot.StartTime;
            decimal temp = 0.5M * (System.Convert.ToDecimal(tspan.TotalMinutes));
            Edit(veh);
        }
        // Set default price of vehicle
        private Vehicle SetDefaultPrice(Vehicle vehicle)
        {
            //reset the parkingPrice to it's default values
            if (vehicle.VehicleType.Category == Category.Car) { }
            else if (vehicle.VehicleType.Category == Category.Mc) { }
            else if (vehicle.VehicleType.Category == Category.Bus) { }
            else if (vehicle.VehicleType.Category == Category.Truck) { }
            else { decimal temp = 3.50M; }

            return vehicle;
        }
        //Remove vehicle from database and return vehicle information
        public Vehicle Remove(int id)
        {
            Vehicle vehicle;
            vehicle = db.Vehicles.Where(v => v.ID == id).FirstOrDefault();

            if (vehicle != null)
            {
                db.Entry(vehicle).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
            {

            }
            return vehicle;
        }
        // Remove by reg nr (not used)
        //public Models.Vehicle Remove(string regNr)
        //{
        //    Models.Vehicle vehicle;
        //    vehicle = db.Vehicles.Where(v => v.RegistrationNumber == regNr).FirstOrDefault();

        //    if (vehicle != null)
        //    {
        //        db.Entry(vehicle).State = EntityState.Deleted;
        //        db.SaveChanges();
        //    }

        //    return vehicle;
        //}
        //Search vehicle(s)
        public List<Vehicle> Search(string searchTerm)
        {
            int pSlot = -1;

            // Try to parse the input string to double,
            // if it works, the user want to search by parking slot
            try
            {
                pSlot = int.Parse(searchTerm);
            }
            catch
            {
                searchTerm = searchTerm.ToUpper();
                
                return db.Vehicles.Where(vehicle => db.Owners.Where(o => o.OwnerName.Contains(searchTerm)).FirstOrDefault() !=null || vehicle.RegNumber == searchTerm).ToList();
            }
            return db.Vehicles.Where(vehicle => vehicle.ParkingSpot.ParkingPlace == pSlot).ToList();
        }

        //public IEnumerable<Vehicle> SortVehicle(string sortOrder)
        //{
        //    var SortVehicle = from v in db.Vehicles
        //                   select v;

        //    switch (sortOrder)
        //    {
        //        case "RegistrationNumber":
        //            SortVehicle = SortVehicle.OrderBy(v => v.RegistrationNumber);
        //            break;
        //        case "Owner":
        //            SortVehicle = SortVehicle.OrderBy(v => v.Owner);
        //            break;
        //        case "Type":
        //            SortVehicle = SortVehicle.OrderBy(v => v.Type);
        //            break;
        //        case "ParkingSpot":
        //            SortVehicle = SortVehicle.OrderBy(v => v.ParkingSpot);
        //            break;
        //        default:
        //            SortVehicle = SortVehicle.OrderBy(v => v.RegistrationNumber);
        //            break;
        //    }
        //    return SortVehicle.ToList();
        //}
    }
}