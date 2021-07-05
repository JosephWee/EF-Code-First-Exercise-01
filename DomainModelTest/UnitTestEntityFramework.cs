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
        public void _EmptyDB()
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
        public void _PopulateDB()
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
    }
}
