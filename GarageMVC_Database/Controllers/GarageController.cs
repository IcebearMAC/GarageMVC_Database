﻿
using GarageMVC_Database.Repository;
using GarageMVC_Database.Models;
using System.Web.Mvc;

namespace GarageMVC_Database.Controllers
{
    public class GarageController : Controller
    {
        GarageRepository garage = new GarageRepository();

        ////GET: Garage
        [HttpGet]
        public ActionResult List(string Search = "")
        {
            return View(garage.GetAll());
        }

        [HttpPost]
        public ActionResult List(string Sort = "", string Filter = "")
        {

            if (Filter == "Car" || Filter == "Bus" || Filter == "Truck" || Filter == "Mc" || Filter == "Other")
            {
                return View(garage.GetFilteredList(Filter));
            }

            Sort = Sort.ToLower();
            if (Sort == "regnumber")
            {
                return View(garage.SortReg(false));
            }
            //else if (Sort == "owner")
            //{
            //    return View(garage.SortOwner(false));
            //}
            else if (Sort == "type")
            {
                return View(garage.SortType(false));
            }
            else if (Sort == "parkingplace")
            {
                return View(garage.SortParking(false));
            }
            return RedirectToAction("Index");
        }


        // GET: Garage/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id != 0)
            {
                //Update the ParkPrice to current price
                garage.UpdateParkPrice();
                return View(garage.GetVehicle(id));
            }
            return RedirectToAction("Index");
        }

        // GET: Garage/Create
        public ActionResult CheckIn()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(garage.Search(""));
        }

        // POST: Garage/Create
        [HttpPost]
        public ActionResult CheckIn(Vehicle vehicle, string vehicleType)
        {
            switch (vehicleType)
            {
                case "Car":
                    vehicle.VehicleType.Category = Category.Car;
                    break;
                case "MC":
                    vehicle.VehicleType.Category = Category.Mc;
                    break;
                case "Bus":
                    vehicle.VehicleType.Category = Category.Bus;
                    break;
                case "Truck":
                    vehicle.VehicleType.Category = Category.Truck;
                    break;
                case "Other":
                    vehicle.VehicleType.Category = Category.Other;
                    break;
                default:
                    return RedirectToAction("Index");
            }

            garage.Add(vehicle);
            return RedirectToAction("Create");
        }

        // GET: Garage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Garage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Garage/Delete/5
        public ActionResult CheckOut(int id = 0)
        {
            if (id != 0)
            {
                garage.UpdateVehiclePrice(id);
                return View(garage.GetVehicle(id));
            }
            return RedirectToAction("Index");
        }


        // POST: Garage/Delete/5
        [HttpPost]
        public ActionResult CheckOut(int id, string value = "")
        {
            garage.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
