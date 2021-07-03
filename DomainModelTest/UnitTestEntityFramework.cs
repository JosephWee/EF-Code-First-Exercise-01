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

        private VehicleRentalContext context = null;

        [TestInitialize]
        public void TestInitialize()
        {
            context = new VehicleRentalContext();
            context.Database.Log = msg => log.Debug(msg);
        }

        [TestMethod]
        public void TestPopulateDB()
        {
            VehicleRentalContextHelper.PopulateDatabase(context);
            context.SaveChanges();

            Assert.IsTrue(context.Countries.Count() == 1);
            Assert.IsTrue(context.States.Count() > 0);
            Assert.IsTrue(context.Cities.Count() > 0);
            Assert.IsTrue(context.Suburbs.Count() > 0);
            Assert.IsTrue(context.Addresses.Count() > 0);
            Assert.IsTrue(context.Locations.Count() > 0);

            Assert.IsTrue(context.VehicleMakes.Count() == 2);
            Assert.IsTrue(context.MotorVehicleModels.Count() == 22);
            Assert.IsTrue(context.MotorVehicles.Count() > 0);

            int fleetCount = 0;
            Dictionary<Ent.MotorVehicleModel, int> dictModelCount = new Dictionary<Ent.MotorVehicleModel, int>();
            foreach (var location in context.Locations)
            {
                fleetCount += location.Fleet.Count;

                var FleetGroupByModels =
                    location.Fleet.GroupBy(x => x.MotorVehicleModel);

                foreach (var FleetOfThisModel in FleetGroupByModels)
                {
                    int existingCount = dictModelCount.ContainsKey(FleetOfThisModel.Key) ? dictModelCount[FleetOfThisModel.Key] : 0;
                    existingCount += FleetOfThisModel.Count();
                }
            }

            var MotorVehiclesGroupByModels =
                context.MotorVehicles.GroupBy(x => x.MotorVehicleModel);

            foreach (var MotorVehiclesOfThisModel in MotorVehiclesGroupByModels)
            {
                int existingCount = dictModelCount.ContainsKey(MotorVehiclesOfThisModel.Key) ? dictModelCount[MotorVehiclesOfThisModel.Key] : int.MinValue;
                Assert.AreNotEqual(existingCount, int.MinValue);
                Assert.AreEqual(existingCount, MotorVehiclesOfThisModel.Count());
            }
        }

        [TestMethod]
        public void TestChangingVehicleMake()
        {
            var vehMakes =
                context.VehicleMakes.ToList();

            Assert.IsTrue(vehMakes.Count(x => x.Name.StartsWith("Star")) > 0);
            Assert.IsTrue(vehMakes.Count(x => x.Name.StartsWith("Global")) > 0);

            //var vehMakeStarAuto = vehMakes.First(x => x.Name.StartsWith("Star"));
            //var vehMakeGlobalVehicle = vehMakes.First(x => x.Name.StartsWith("Global"));

            //var modelStarAuto =
            //    context
            //    .MotorVehicleModels
            //    .FirstOrDefault(x => x.VehicleMakeId == vehMakeStarAuto.VehicleMakeId);

            //Assert.IsNotNull(modelStarAuto);

            //var modelGlobalVehicle =
            //    context
            //    .MotorVehicleModels
            //    .FirstOrDefault(x => x.VehicleMakeId == vehMakeGlobalVehicle.VehicleMakeId);

            //Assert.IsNotNull(modelGlobalVehicle);

            //modelStarAuto.VehicleMake = modelGlobalVehicle.VehicleMake;

            //Assert.AreEqual(modelStarAuto.VehicleMakeId, modelGlobalVehicle.VehicleMakeId);
        }
    }
}
