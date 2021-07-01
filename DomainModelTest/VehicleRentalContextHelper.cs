using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent = DomainModel.Entities;

namespace DomainModelTest
{
    public class VehicleRentalContextHelper
    {
        private static Random rand = new Random();

        public static void ClearDatabase(DomainModel.VehicleRentalContext context)
        {
            var vehicleMakesToDelete =
                context.VehicleMakes.ToList();
            context.VehicleMakes.RemoveRange(vehicleMakesToDelete);

            context.SaveChanges();

            var CountriesToDelete =
                context.Countries.ToList();
            context.Countries.RemoveRange(CountriesToDelete);

            context.SaveChanges();
        }

        public static void PopulateDatabase(DomainModel.VehicleRentalContext context)
        {
            //Use Fictional Make
            var make_StarAuto = new Ent.VehicleMake() { VehicleMakeId = new Guid("8A103C71-1675-46F8-BDE8-4637CAF656F1"), Name = "StarAuto" };
            var make_GlobalVehicle = new Ent.VehicleMake() { VehicleMakeId = new Guid("794F01F1-0D0F-4BBC-8649-9DC8AF7E0C23"), Name = "GlobalVehicle" };

            context.VehicleMakes.AddOrUpdate(
                x => x.VehicleMakeId,
                make_StarAuto,
                make_GlobalVehicle
            );

            //Use Fictional Models
            var model_StarAuto_Light_Hatch =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("1D4F8989-FBF0-4738-A9E3-9F45D27A38AC"),
                    VehicleMake = make_StarAuto,
                    Name = "Light-HA",
                    MotorVehicleType = Ent.MotorVehicleType.HatchBack,
                    FuelCapacity = 1.4,
                    FuelConsumption = 5.8,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 80,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_StarAuto_Light_Sedan =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("4C326F79-9AE3-4F0B-B9A9-00F75BD60CE9"),
                    VehicleMake = make_StarAuto,
                    Name = "Light-SA",
                    MotorVehicleType = Ent.MotorVehicleType.Sedan,
                    FuelCapacity = 1.4,
                    FuelConsumption = 5.8,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 80,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_StarAuto_Mid_Hatch =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("6C0472D4-BC69-48BE-8212-47175884D0FC"),
                    VehicleMake = make_StarAuto,
                    Name = "Midnight-HA",
                    MotorVehicleType = Ent.MotorVehicleType.HatchBack,
                    FuelCapacity = 2.5,
                    FuelConsumption = 6.5,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 154,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_StarAuto_Mid_Sedan =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("E04B8706-640A-4206-B4E1-2F06BBE70C84"),
                    VehicleMake = make_StarAuto,
                    Name = "Midnight-SA",
                    MotorVehicleType = Ent.MotorVehicleType.Sedan,
                    FuelCapacity = 2.5,
                    FuelConsumption = 6.5,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 154,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_StarAuto_Mid_Hybrid =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("19AA1F11-B4C7-4A3D-88A6-B428449DC6B2"),
                    VehicleMake = make_StarAuto,
                    Name = "Midnight-HH",
                    MotorVehicleType = Ent.MotorVehicleType.HatchBack,
                    FuelCapacity = 1.7,
                    FuelConsumption = 3.9,
                    EngineType = Ent.EngineType.Hybrid_Electric_Petrol,
                    MaxPower = 100,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_StarAuto_CompactSUV =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("C5548210-DBA9-436D-9CB2-AC9D47C38BAB"),
                    VehicleMake = make_StarAuto,
                    Name = "Comet-A",
                    MotorVehicleType = Ent.MotorVehicleType.CompactSUV,
                    FuelCapacity = 2.5,
                    FuelConsumption = 7.0,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 160,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = true
                };

            var model_StarAuto_SUV =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("E1D74FAF-8DD8-42AB-94AD-6E488C2A7A47"),
                    VehicleMake = make_StarAuto,
                    Name = "Storm-A",
                    MotorVehicleType = Ent.MotorVehicleType.SUV,
                    FuelCapacity = 2.0,
                    FuelConsumption = 6.0,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 127,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = true
                };

            var model_StarAuto_MiniBus =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("E54E62A1-E032-471D-96DC-A7B0CE9C9341"),
                    VehicleMake = make_StarAuto,
                    Name = "Forrest-A",
                    MotorVehicleType = Ent.MotorVehicleType.MiniBus,
                    FuelCapacity = 2.5,
                    FuelConsumption = 8.5,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 135,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = true
                };

            var model_StarAuto_VanManual =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("BA484D5A-3120-4E9E-B2BF-8DDDD0459298"),
                    VehicleMake = make_StarAuto,
                    Name = "Cargo-VM",
                    MotorVehicleType = Ent.MotorVehicleType.Van,
                    FuelCapacity = 2.5,
                    FuelConsumption = 5.0,
                    EngineType = Ent.EngineType.Diesel,
                    MaxPower = 106,
                    TransmissionType = Ent.TransmissionType.Manual,
                    AWD = false,
                    CargoVolume = 2982
                };

            var model_StarAuto_VanAuto =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("D9236900-1DA7-48DD-9A66-66958A4BA977"),
                    VehicleMake = make_StarAuto,
                    Name = "Cargo-VA",
                    MotorVehicleType = Ent.MotorVehicleType.Van,
                    FuelCapacity = 2.5,
                    FuelConsumption = 5.0,
                    EngineType = Ent.EngineType.Diesel,
                    MaxPower = 106,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false,
                    CargoVolume = 2982
                };

            var model_StarAuto_Pickup =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("58901907-16D9-49A1-8BA4-C4901FC8433D"),
                    VehicleMake = make_StarAuto,
                    Name = "Taurus-PM",
                    MotorVehicleType = Ent.MotorVehicleType.Pickup,
                    FuelCapacity = 2.5,
                    FuelConsumption = 8.0,
                    EngineType = Ent.EngineType.Diesel,
                    MaxPower = 110,
                    TransmissionType = Ent.TransmissionType.Manual,
                    AWD = false,
                    CargoVolume = 2393
                };

            var model_GlobalVehicle_Light_Hatch =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("A66A0835-A175-4E95-A3D4-D410BB0961A3"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Agile-HA",
                    MotorVehicleType = Ent.MotorVehicleType.HatchBack,
                    FuelCapacity = 1.4,
                    FuelConsumption = 5.9,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 82,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_GlobalVehicle_Light_Sedan =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("78F1A964-7038-41BE-9DA5-0F04FFC399E6"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Agile-SA",
                    MotorVehicleType = Ent.MotorVehicleType.Sedan,
                    FuelCapacity = 1.4,
                    FuelConsumption = 5.9,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 82,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_GlobalVehicle_Mid_Hatch =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("3C05C36C-94A3-4038-84AA-0A6D2048C9B5"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Central-HA",
                    MotorVehicleType = Ent.MotorVehicleType.HatchBack,
                    FuelCapacity = 2.5,
                    FuelConsumption = 6.6,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 155,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_GlobalVehicle_Mid_Sedan =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("ABFFFDB6-114A-44BF-9A4E-43C371D59BDE"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Central-SA",
                    MotorVehicleType = Ent.MotorVehicleType.Sedan,
                    FuelCapacity = 2.5,
                    FuelConsumption = 6.6,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 155,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_GlobalVehicle_Mid_Hybrid =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("46289B68-BF84-4C6D-8303-668A39E5FD92"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Central-HH",
                    MotorVehicleType = Ent.MotorVehicleType.HatchBack,
                    FuelCapacity = 1.7,
                    FuelConsumption = 4.0,
                    EngineType = Ent.EngineType.Hybrid_Electric_Petrol,
                    MaxPower = 105,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false
                };

            var model_GlobalVehicle_CompactSUV =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("75A0CA9C-1769-405D-918D-C4D41C82C076"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Scout-A",
                    MotorVehicleType = Ent.MotorVehicleType.CompactSUV,
                    FuelCapacity = 2.5,
                    FuelConsumption = 7.2,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 165,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = true
                };

            var model_GlobalVehicle_SUV =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("E1244EC9-F9EE-4C30-9126-68D9CA5FF51A"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Explore-A",
                    MotorVehicleType = Ent.MotorVehicleType.SUV,
                    FuelCapacity = 2.0,
                    FuelConsumption = 6.2,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 130,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = true
                };

            var model_GlobalVehicle_MiniBus =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("2094F987-423A-451A-BF22-8F0EBF93AAA2"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Social-A",
                    MotorVehicleType = Ent.MotorVehicleType.MiniBus,
                    FuelCapacity = 2.5,
                    FuelConsumption = 8.8,
                    EngineType = Ent.EngineType.Petrol,
                    MaxPower = 140,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = true
                };

            var model_GlobalVehicle_VanManual =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("C9C3B06F-86A2-438E-96FC-6DD21A9969A4"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Caravan-VM",
                    MotorVehicleType = Ent.MotorVehicleType.Van,
                    FuelCapacity = 2.5,
                    FuelConsumption = 5.5,
                    EngineType = Ent.EngineType.Diesel,
                    MaxPower = 110,
                    TransmissionType = Ent.TransmissionType.Manual,
                    AWD = false,
                    CargoVolume = 2992
                };

            var model_GlobalVehicle_VanAuto =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("3DCDC64C-76DE-484A-8B35-6C7476AF5E85"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Caravan-VA",
                    MotorVehicleType = Ent.MotorVehicleType.Van,
                    FuelCapacity = 2.5,
                    FuelConsumption = 5.5,
                    EngineType = Ent.EngineType.Diesel,
                    MaxPower = 110,
                    TransmissionType = Ent.TransmissionType.Auto,
                    AWD = false,
                    CargoVolume = 2992
                };

            var model_GlobalVehicle_Pickup =
                new Ent.MotorVehicleModel()
                {
                    MotorVehicleModelId = new Guid("09F010DE-2910-4322-9909-A20DD804AA7D"),
                    VehicleMake = make_GlobalVehicle,
                    Name = "Atlas-PM",
                    MotorVehicleType = Ent.MotorVehicleType.Pickup,
                    FuelCapacity = 2.5,
                    FuelConsumption = 8.4,
                    EngineType = Ent.EngineType.Diesel,
                    MaxPower = 120,
                    TransmissionType = Ent.TransmissionType.Manual,
                    AWD = false,
                    CargoVolume = 2495
                };

            context.MotorVehicleModels.AddOrUpdate(
                x => x.MotorVehicleModelId,
                model_StarAuto_Light_Hatch,
                model_StarAuto_Light_Sedan,
                model_StarAuto_Mid_Hatch,
                model_StarAuto_Mid_Sedan,
                model_StarAuto_Mid_Hybrid,
                model_StarAuto_CompactSUV,
                model_StarAuto_SUV,
                model_StarAuto_MiniBus,
                model_StarAuto_VanManual,
                model_StarAuto_VanAuto,
                model_StarAuto_Pickup,
                model_GlobalVehicle_Light_Hatch,
                model_GlobalVehicle_Light_Sedan,
                model_GlobalVehicle_Mid_Hatch,
                model_GlobalVehicle_Mid_Sedan,
                model_GlobalVehicle_Mid_Hybrid,
                model_GlobalVehicle_CompactSUV,
                model_GlobalVehicle_SUV,
                model_GlobalVehicle_MiniBus,
                model_GlobalVehicle_VanManual,
                model_GlobalVehicle_VanAuto,
                model_GlobalVehicle_Pickup
            );

            context.SaveChanges();

            Queue<Ent.MotorVehicleModel> qModels = new Queue<Ent.MotorVehicleModel>();
            qModels.Enqueue(model_StarAuto_Light_Hatch);
            qModels.Enqueue(model_StarAuto_Light_Sedan);
            qModels.Enqueue(model_StarAuto_Mid_Hatch);
            qModels.Enqueue(model_StarAuto_Mid_Sedan);
            qModels.Enqueue(model_StarAuto_Mid_Hybrid);
            qModels.Enqueue(model_StarAuto_CompactSUV);
            qModels.Enqueue(model_StarAuto_SUV);
            qModels.Enqueue(model_StarAuto_MiniBus);
            qModels.Enqueue(model_StarAuto_VanManual);
            qModels.Enqueue(model_StarAuto_VanAuto);
            qModels.Enqueue(model_StarAuto_Pickup);
            qModels.Enqueue(model_GlobalVehicle_Light_Hatch);
            qModels.Enqueue(model_GlobalVehicle_Light_Sedan);
            qModels.Enqueue(model_GlobalVehicle_Mid_Hatch);
            qModels.Enqueue(model_GlobalVehicle_Mid_Sedan);
            qModels.Enqueue(model_GlobalVehicle_Mid_Hybrid);
            qModels.Enqueue(model_GlobalVehicle_CompactSUV);
            qModels.Enqueue(model_GlobalVehicle_SUV);
            qModels.Enqueue(model_GlobalVehicle_MiniBus);
            qModels.Enqueue(model_GlobalVehicle_VanManual);
            qModels.Enqueue(model_GlobalVehicle_VanAuto);
            qModels.Enqueue(model_GlobalVehicle_Pickup);

            //I chose a rather simplistic model for Address, Country, State, City and Suburbs
            //This is decided for simplicity's sake and I am not actually working with any
            //domain expert users (Jimmy Nilson: Applying Domain-Driven Design and Patterns) 

            //Fictional Country "Knotreel" and it's States, Cities and Suburbs
            //"Knotreel" is, at the time of creating this code, not listed in https://en.wikipedia.org/wiki/List_of_fictional_countries
            //However please inform me if it is already copyrighted and I will change it

            List<string> countryNames = new List<string>() { "Knotreel" };
            Queue<Tuple<string, bool>> regionNames = new Queue<Tuple<string, bool>>();
            regionNames.Enqueue(new Tuple<string, bool>("North", true));
            regionNames.Enqueue(new Tuple<string, bool>("South", true));
            regionNames.Enqueue(new Tuple<string, bool>("East", true));
            regionNames.Enqueue(new Tuple<string, bool>("West", true));
            regionNames.Enqueue(new Tuple<string, bool>("Central", false));

            int rounds = rand.Next(0, regionNames.Count);
            for (int sh = 0; sh < rounds; sh++)
            {
                var entry = regionNames.Dequeue();
                regionNames.Enqueue(entry);
            }

            List<Tuple<string, bool>> regionTypes = new List<Tuple<string, bool>>()
            {
                new Tuple<string, bool>("Coasts", true),
                new Tuple<string, bool>("Peaks", false),
                new Tuple<string, bool>("Ridges", false),
                new Tuple<string, bool>("Hills", false),
                new Tuple<string, bool>("Plains", false)
            };

            List<string> cityNames_Part1 = new List<string>() { "Winter", "Haven", "Storm", "Frost", "Wind" };
            List<string> cityNames_Part2 = new List<string>() { "City", "Hold", "Deep" };

            List<string> suburbNames = new List<string>() { "Blue", "Green", "Maroon", "Amber", "Minners", "Merchants", "Traders", "Market", "Castle", "Monument", "Capitol" };
            List<string> suburbTypes = new List<string>() { "District", "Circle", "Gardens", "Square", "Walk", "Gate" };

            List<string> streetNames = new List<string>()
            {
                "Main",
                "Station",
                "Tailor",
                "Bond",
                "Orchard",
                "Orange",
                "Oak",
                "Acacia",
                "Copper",
                "Helm",
                "Watt",
                "Wallace",
                "Toll",
                "Blacksmith",
                "Kings",
                "River",
                "Fence",
                "Lakeview",
                "Cart",
                "Mountain View"
            };

            List<string> streetTypes = new List<string>()
            {
                "Street",
                "Road",
                "Rise",
                "Heights",
                "Grove"
            };

            List<string> usedCountryNames = new List<string>();
            List<string> usedStates = new List<string>();
            List<string> usedCities = new List<string>();
            List<string> usedSuburbs = new List<string>();
            List<string> usedStreets = new List<string>();
            List<string> usedVIN = new List<string>();
            List<string> usedRegistration = new List<string>();

            int stateCount = 7;
            int suburbCount = 5;
            for (int c = 0; c < countryNames.Count; c++)
            {
                var country =
                    new Ent.Country()
                    {
                        CountryId = Guid.NewGuid(),
                        Name = countryNames[c]
                    };
                context.Countries.AddOrUpdate(country);

                for (int st = 0; st < stateCount; st++)
                {
                    var state =
                        new Ent.State()
                        {
                            StateId = Guid.NewGuid(),
                            CountryId = country.CountryId,
                            Name = GenerateStateName(usedStates, regionNames, regionTypes),
                        };
                    context.States.AddOrUpdate(state);

                    var city =
                        new Ent.City()
                        {
                            CityId = Guid.NewGuid(),
                            StateId = state.StateId,
                            Name = GenerateCityName(usedCities, cityNames_Part1, cityNames_Part2),
                        };
                    context.Cities.AddOrUpdate(city);

                    for (int b = 0; b < suburbCount; b++)
                    {
                        int streetNumber = rand.Next(1, 30);
                        int parkingCapacity = 15 + rand.Next(5, 20);
                        string postalcode = $"{c:0}{st:0}{(st + rand.Next(0, 5)):00}{b:00}{streetNumber:00}";

                        var suburb =
                            new Ent.Suburb()
                            {
                                SuburbId = Guid.NewGuid(),
                                CityId = city.CityId,
                                Name = GenerateSuburbName(usedSuburbs, suburbNames, suburbTypes),
                            };
                        context.Suburbs.AddOrUpdate(suburb);

                        var address =
                            new Ent.Address()
                            {
                                AddressId = Guid.NewGuid(),
                                SuburbId = suburb.SuburbId,
                                AddressLine1 = string.Format("{0} {1}", streetNumber, GenerateStreetName(usedStreets, streetNames, streetTypes)),
                                PostalCode = postalcode
                            };
                        context.Addresses.AddOrUpdate(address);

                        var location =
                            new Ent.Location()
                            {
                                LocationId = Guid.NewGuid(),
                                Name = usedStreets.Last(),
                                Address = address,
                                ParkingCapacity = parkingCapacity
                            };
                        context.Locations.AddOrUpdate(location);

                        for (int p = 0; p < parkingCapacity; p++)
                        {
                            Ent.MotorVehicleModel vehModel = qModels.Dequeue();

                            int Year = 2010 + rand.Next(0, 11);
                            int Mileage = rand.Next(0, 200000);

                            Ent.PickupCargoAccessoryType PickupCargoAccessoryType = Ent.PickupCargoAccessoryType.None;
                            if (vehModel.MotorVehicleType == Ent.MotorVehicleType.Pickup)
                            {
                                int r = rand.Next(1, 100);
                                if (r >= 33 && r <= 66)
                                {
                                    PickupCargoAccessoryType = Ent.PickupCargoAccessoryType.TonneauCover;
                                }
                                else if (r > 66)
                                {
                                    PickupCargoAccessoryType = Ent.PickupCargoAccessoryType.TruckCap;
                                }
                            }

                            Ent.MotorVehicle vehicle = new Ent.MotorVehicle()
                            {
                                Id = Guid.NewGuid(),
                                MotorVehicleModel = vehModel,
                                Year = Year,
                                VIN = GenerateVIN(usedVIN),
                                Registration = GenerateRegistration(usedRegistration),
                                Mileage = Mileage,
                                HasNavSystem = rand.Next(1, 100) >= 50,
                                HasDashCam = rand.Next(1, 100) >= 50,
                                HasBlindspotMonitoring = rand.Next(1, 100) >= 50,
                                HasAutomaticEmergencyBrake = rand.Next(1, 100) >= 50,
                                HasReversingCam = rand.Next(1, 100) >= 50,
                                HasForwardParkingSensor = rand.Next(1, 100) >= 50,
                                HasRearParkingSensor = rand.Next(1, 100) >= 50,
                                PickupCargoAccessoryType = PickupCargoAccessoryType
                            };
                            context.MotorVehicles.AddOrUpdate(vehicle);
                            location.Fleet.Add(vehicle);

                            qModels.Enqueue(vehModel);
                        }
                    }
                }
            }

            context.SaveChanges();
        }

        private static string GenerateStateName(
            List<string> usedStates,
            Queue<Tuple<string, bool>> regionNames,
            List<Tuple<string, bool>> regionTypes)
        {
            string stateName = null;

            do
            {
                var regionName = regionNames.Dequeue();

                if (regionName.Item2)
                {
                    int index = rand.Next(0, regionTypes.Count - 1);
                    stateName =
                        string.Format(
                            "{0} {1}",
                            regionName.Item1,
                            regionTypes.ElementAt(index).Item1);
                }
                else
                {
                    int index = rand.Next(0, regionTypes.Count(x => x.Item2 == false) - 1);
                    stateName =
                        string.Format(
                            "{0} {1}",
                            regionName.Item1,
                            regionTypes.Where(x => x.Item2 == false).ElementAt(index).Item1);
                }

                regionNames.Enqueue(regionName);
            }
            while (usedStates.Contains(stateName));

            if (!string.IsNullOrEmpty(stateName))
                usedStates.Add(stateName);

            return stateName;
        }

        private static string GenerateCityName(
            List<string> usedCities,
            List<string> cityNames_Part1,
            List<string> cityNames_Part2)
        {
            string cityName = null;
            do
            {
                cityName =
                    string.Format(
                        "{0} {1}",
                        cityNames_Part1[rand.Next(0, cityNames_Part1.Count - 1)],
                        cityNames_Part2[rand.Next(0, cityNames_Part2.Count - 1)]
                    );
            } while (usedCities.Contains(cityName));

            if (!string.IsNullOrEmpty(cityName))
                usedCities.Add(cityName);

            return cityName;
        }

        private static string GenerateSuburbName(
            List<string> usedSuburbs,
            List<string> suburbNames,
            List<string> suburbTypes)
        {
            string suburbName = null;
            do
            {
                suburbName =
                    string.Format(
                        "{0} {1}",
                        suburbNames[rand.Next(0, suburbNames.Count - 1)],
                        suburbTypes[rand.Next(0, suburbTypes.Count - 1)]
                    );
            } while (usedSuburbs.Contains(suburbName));

            if (!string.IsNullOrEmpty(suburbName))
                usedSuburbs.Add(suburbName);

            return suburbName;
        }

        private static string GenerateStreetName(
            List<string> usedStreets,
            List<string> streetNames,
            List<string> streetTypes)
        {
            string streetName = null;
            do
            {
                streetName =
                    string.Format(
                        "{0} {1}",
                        streetNames[rand.Next(0, streetNames.Count - 1)],
                        streetTypes[rand.Next(0, streetTypes.Count - 1)]
                    );
            } while (usedStreets.Contains(streetName));

            if (!string.IsNullOrEmpty(streetName))
                usedStreets.Add(streetName);

            return streetName;
        }

        private static string GenerateVIN(List<string> usedVIN)
        {
            string VIN = null;
            do
            {
                StringBuilder builderVIN = new StringBuilder();

                for (int vin = 0; vin < 17; vin++)
                {
                    if (vin == 0 || vin == 3 || vin == 9)
                    {
                        builderVIN.Append(Convert.ToChar(rand.Next(65, 87)));
                    }
                    else
                    {
                        builderVIN.Append(rand.Next(0, 9));
                    }
                }

                VIN = builderVIN.ToString();

            } while (usedVIN.Contains(VIN));

            if (!string.IsNullOrEmpty(VIN))
                usedVIN.Add(VIN);

            return VIN;
        }

        private static string GenerateRegistration(List<string> usedRegistration)
        {
            string RegistrationNumber = null;

            do
            {
                RegistrationNumber =
                    string.Format(
                        "S{0}{1}{2:0000}{3}",
                        Convert.ToChar(rand.Next(65, 87)),
                        Convert.ToChar(rand.Next(65, 87)),
                        rand.Next(0, 1000),
                        Convert.ToChar(rand.Next(65, 87))
                    );
            } while (usedRegistration.Contains(RegistrationNumber));

            if (!string.IsNullOrEmpty(RegistrationNumber))
                usedRegistration.Add(RegistrationNumber);

            return RegistrationNumber;
        }
    }
}
