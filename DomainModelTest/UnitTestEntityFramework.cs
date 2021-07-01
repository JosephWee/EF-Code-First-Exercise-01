using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using DomainModel;
using Ent = DomainModel.Entities;

namespace DomainModelTest
{
    [TestClass]
    public class UnitTestEntityFramework
    {
        private VehicleRentalContext context = null;

        [TestInitialize]
        public void TestInitialize()
        {
            context = new VehicleRentalContext();
        }

        [TestMethod]
        public void TestChangingVehicleMake()
        {
            var vehMakes =
                context.VehicleMakes.ToList();

            Assert.IsTrue(vehMakes.Count(x => x.Name.StartsWith("Star")) > 0);
            Assert.IsTrue(vehMakes.Count(x => x.Name.StartsWith("Global")) > 0);

            var vehMakeStarAuto = vehMakes.First(x => x.Name.StartsWith("Star"));
            var vehMakeGlobalVehicle = vehMakes.First(x => x.Name.StartsWith("Global"));

            var modelStarAuto =
                context
                .MotorVehicleModels
                .FirstOrDefault(x => x.VehicleMakeId == vehMakeStarAuto.VehicleMakeId);

            Assert.IsNotNull(modelStarAuto);

            var modelGlobalVehicle =
                context
                .MotorVehicleModels
                .FirstOrDefault(x => x.VehicleMakeId == vehMakeGlobalVehicle.VehicleMakeId);

            Assert.IsNotNull(modelGlobalVehicle);

            modelStarAuto.VehicleMake = modelGlobalVehicle.VehicleMake;

            Assert.AreEqual(modelStarAuto.VehicleMakeId, modelGlobalVehicle.VehicleMakeId);
        }
    }
}
