using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using DomainModel;
using Ent = DomainModel.Entities;
using System.Collections.Generic;

namespace DomainModelTest
{
    [TestClass]
    public class UnitTestEntityFramework
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        [TestCategory("EF6 - Basic")]
        [TestCategory("EF6 - Disconnected Scenario")]
        public void EmptyDB()
        {
            var context = new VehicleRentalContext();

            Assert.IsTrue(context.MotorVehicles.Count() > 0);
            Assert.IsTrue(context.MotorVehicleModels.Count() > 0);
            Assert.IsTrue(context.VehicleMakes.Count() > 0);

            Assert.IsTrue(context.Locations.Count() > 0);
            Assert.IsTrue(context.Addresses.Count() > 0);
            Assert.IsTrue(context.Suburbs.Count() > 0);
            Assert.IsTrue(context.Cities.Count() > 0);
            Assert.IsTrue(context.States.Count() > 0);
            Assert.IsTrue(context.Countries.Count() > 0);
            
            VehicleRentalContextHelper.EmptyDatabase(log);

            Assert.IsTrue(context.MotorVehicles.Count() == 0);
            Assert.IsTrue(context.MotorVehicleModels.Count() == 0);
            Assert.IsTrue(context.VehicleMakes.Count() == 0);

            Assert.IsTrue(context.Locations.Count() == 0);
            Assert.IsTrue(context.Addresses.Count() == 0);
            Assert.IsTrue(context.Suburbs.Count() == 0);
            Assert.IsTrue(context.Cities.Count() == 0);
            Assert.IsTrue(context.States.Count() == 0);
            Assert.IsTrue(context.Countries.Count() == 0);
        }

        [TestMethod]
        [TestCategory("EF6 - Basic")]
        [TestCategory("EF6 - Disconnected Scenario")]
        public void PopulateDB()
        {
            var context = new VehicleRentalContext();

            Assert.IsTrue(context.MotorVehicles.Count() == 0);
            Assert.IsTrue(context.MotorVehicleModels.Count() == 0);
            Assert.IsTrue(context.VehicleMakes.Count() == 0);

            Assert.IsTrue(context.Locations.Count() == 0);
            Assert.IsTrue(context.Addresses.Count() == 0);
            Assert.IsTrue(context.Suburbs.Count() == 0);
            Assert.IsTrue(context.Cities.Count() == 0);
            Assert.IsTrue(context.States.Count() == 0);
            Assert.IsTrue(context.Countries.Count() == 0);

            VehicleRentalContextHelper.PopulateDatabase(log);

            Assert.IsTrue(context.MotorVehicles.Count() > 0);
            Assert.IsTrue(context.MotorVehicleModels.Count() > 0);
            Assert.IsTrue(context.VehicleMakes.Count() > 0);

            Assert.IsTrue(context.Locations.Count() > 0);
            Assert.IsTrue(context.Addresses.Count() > 0);
            Assert.IsTrue(context.Suburbs.Count() > 0);
            Assert.IsTrue(context.Cities.Count() > 0);
            Assert.IsTrue(context.States.Count() > 0);
            Assert.IsTrue(context.Countries.Count() > 0);

            int fleetCount = 0;
            Dictionary<Guid, int> dictModelCount = new Dictionary<Guid, int>();
            foreach (var location in context.Locations.ToList())
            {
                fleetCount += location.Fleet.Count();

                var gByModel_Fleet =
                    location.Fleet.GroupBy(x => x.MotorVehicleModel).ToList();

                foreach (var fleetForModel in gByModel_Fleet)
                {
                    if (dictModelCount.ContainsKey(fleetForModel.Key.MotorVehicleModelId))
                    {
                        int existingCount =
                            dictModelCount[fleetForModel.Key.MotorVehicleModelId] + fleetForModel.Count();
                        dictModelCount[fleetForModel.Key.MotorVehicleModelId] = existingCount;
                    }
                    else
                    {
                        dictModelCount[fleetForModel.Key.MotorVehicleModelId] = fleetForModel.Count();
                    }
                }
            }

            var gByModel_Vehicles =
                context.MotorVehicles.GroupBy(x => x.MotorVehicleModel).ToList();

            foreach (var vehiclesForModel in gByModel_Vehicles)
            {
                int existingCount = dictModelCount.ContainsKey(vehiclesForModel.Key.MotorVehicleModelId) ? dictModelCount[vehiclesForModel.Key.MotorVehicleModelId] : int.MinValue;
                Assert.AreNotEqual(existingCount, int.MinValue);
                Assert.AreEqual(existingCount, vehiclesForModel.Count());
            }
        }

        [TestMethod]
        [TestCategory("EF6 - Connected Scenario")]
        [TestCategory("EF6 - Update")]
        public void ChangeVehicleMake()
        {
            var context = new VehicleRentalContext();

            Assert.IsTrue(context.VehicleMakes.Count() >= 2);
            Assert.IsTrue(context.MotorVehicleModels.Count() > 0);

            var vehicleMakes = context.VehicleMakes.ToList();
            var vehicleModel = context.MotorVehicleModels.First();
            var vehicleMake = vehicleModel.VehicleMake;
            var otherVehicleMake = vehicleMakes.First(m => m.VehicleMakeId != vehicleMake.VehicleMakeId);

            vehicleModel.VehicleMakeId = otherVehicleMake.VehicleMakeId;

            context.SaveChanges();

            var vehicleModelAfterSave = context.MotorVehicleModels.First(m => m.MotorVehicleModelId == vehicleModel.MotorVehicleModelId);

            Assert.IsNotNull(vehicleModelAfterSave);
            Assert.AreEqual(vehicleModel.MotorVehicleModelId, vehicleModelAfterSave.MotorVehicleModelId);
            Assert.AreNotEqual(vehicleMake.VehicleMakeId, vehicleModelAfterSave.VehicleMakeId);
            Assert.AreEqual(otherVehicleMake.VehicleMakeId, vehicleModelAfterSave.VehicleMakeId);
        }

        [TestMethod]
        [TestCategory("EF6 - Connected Scenario")]
        [TestCategory("EF6 - Update")]
        public void RemoveVehicleFromFleet()
        {
            var context = new VehicleRentalContext();

            Assert.IsTrue(context.Locations.Count() > 0);
            Assert.IsTrue(context.MotorVehicles.Count(mv => mv.LocationId != null) > 0);

            var location = context.Locations.First(L => L.Fleet.Count() > 0);
            var fleetVehicle = location.Fleet.First();
            Guid? oldLocationId = fleetVehicle.LocationId;
            location.Fleet.Remove(fleetVehicle);
            
            context.SaveChanges();

            var vehicleAfterSave = context.MotorVehicles.First(mv => mv.Id == fleetVehicle.Id);

            Assert.IsNotNull(vehicleAfterSave);
            Assert.IsTrue(oldLocationId.HasValue);
            Assert.IsTrue(oldLocationId != Guid.Empty);
            Assert.IsNull(vehicleAfterSave.LocationId);
            Assert.IsNull(vehicleAfterSave.Location);
        }

        [TestMethod]
        [TestCategory("EF6 - Connected Scenario")]
        [TestCategory("EF6 - Update")]
        [TestCategory("EF6 - Delete")]
        [TestCategory("EF6 - Cascade Set Null")]
        public void DeleteLocationWithFleet()
        {
            var context = new VehicleRentalContext();

            Assert.IsTrue(context.Locations.Count(L => L.Fleet.Count() > 0) > 0);
            
            var locationWithFleet = context.Locations.First(L => L.Fleet.Count() > 0);
            var fleetIds = locationWithFleet.Fleet.Select(v => v.Id).ToList();
            Assert.IsTrue(fleetIds.Count > 0);

            context.Locations.Remove(locationWithFleet);

            context.SaveChanges();

            var locationAfterSave = context.Locations.FirstOrDefault(L => L.LocationId == locationWithFleet.LocationId);
            var formerFleetVehicles = context.MotorVehicles.Where(mv => fleetIds.Contains(mv.Id)).ToList();

            Assert.IsNull(locationAfterSave);
            Assert.AreEqual(fleetIds.Count, formerFleetVehicles.Count);
            Assert.AreEqual(fleetIds.Count, formerFleetVehicles.Count(mv => mv.LocationId == null));
        }

        [TestMethod]
        [TestCategory("EF6 - Connected Scenario")]
        [TestCategory("EF6 - Delete")]
        [TestCategory("EF6 - Cascade Set Null")]
        [TestCategory("EF6 - Cascade Delete")]
        public void DeleteCountryThenMake()
        {
            var context = new VehicleRentalContext();

            Assert.IsTrue(context.MotorVehicles.Count() > 0);
            Assert.IsTrue(context.MotorVehicleModels.Count() > 0);
            Assert.IsTrue(context.VehicleMakes.Count() > 0);

            Assert.IsTrue(context.Locations.Count() > 0);
            Assert.IsTrue(context.Addresses.Count() > 0);
            Assert.IsTrue(context.Suburbs.Count() > 0);
            Assert.IsTrue(context.Cities.Count() > 0);
            Assert.IsTrue(context.States.Count() > 0);
            Assert.IsTrue(context.Countries.Count() > 0);

            context.Countries.RemoveRange(
                context.Countries.ToList()
            );

            context.SaveChanges();

            Assert.IsTrue(context.MotorVehicles.Count() > 0);
            Assert.IsTrue(context.MotorVehicleModels.Count() > 0);
            Assert.IsTrue(context.VehicleMakes.Count() > 0);

            Assert.IsTrue(context.Locations.Count() == 0);
            Assert.IsTrue(context.Addresses.Count() == 0);
            Assert.IsTrue(context.Suburbs.Count() == 0);
            Assert.IsTrue(context.Cities.Count() == 0);
            Assert.IsTrue(context.States.Count() == 0);
            Assert.IsTrue(context.Countries.Count() == 0);

            context.VehicleMakes.RemoveRange(
                context.VehicleMakes.ToList()
            );

            context.SaveChanges();

            Assert.IsTrue(context.MotorVehicles.Count() == 0);
            Assert.IsTrue(context.MotorVehicleModels.Count() == 0);
            Assert.IsTrue(context.VehicleMakes.Count() == 0);

            Assert.IsTrue(context.Locations.Count() == 0);
            Assert.IsTrue(context.Addresses.Count() == 0);
            Assert.IsTrue(context.Suburbs.Count() == 0);
            Assert.IsTrue(context.Cities.Count() == 0);
            Assert.IsTrue(context.States.Count() == 0);
            Assert.IsTrue(context.Countries.Count() == 0);
        }
    }
}
