namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Guid(nullable: false),
                        AddressLine1 = c.String(nullable: false, maxLength: 50),
                        AddressLine2 = c.String(maxLength: 50),
                        PostalCode = c.String(nullable: false),
                        Suburb_SuburbId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Suburbs", t => t.Suburb_SuburbId, cascadeDelete: true)
                .Index(t => t.Suburb_SuburbId);
            
            CreateTable(
                "dbo.Suburbs",
                c => new
                    {
                        SuburbId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        City_CityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SuburbId)
                .ForeignKey("dbo.Cities", t => t.City_CityId, cascadeDelete: true)
                .Index(t => t.City_CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        State_StateId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.State_StateId, cascadeDelete: true)
                .Index(t => t.State_StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Country_CountryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId, cascadeDelete: true)
                .Index(t => t.Country_CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        ParkingCapacity = c.Int(nullable: false),
                        Address_AddressId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId, cascadeDelete: true)
                .Index(t => t.Name)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.MotorVehicles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Year = c.Int(nullable: false),
                        VIN = c.String(nullable: false, maxLength: 17),
                        Registration = c.String(maxLength: 8),
                        Mileage = c.Int(nullable: false),
                        HasNavSystem = c.Boolean(nullable: false),
                        HasDashCam = c.Boolean(nullable: false),
                        HasReversingCam = c.Boolean(nullable: false),
                        HasForwardParkingSensor = c.Boolean(nullable: false),
                        HasRearParkingSensor = c.Boolean(nullable: false),
                        HasBlindspotMonitoring = c.Boolean(nullable: false),
                        HasAutomaticEmergencyBrake = c.Boolean(nullable: false),
                        PickupCargoAccessoryType = c.Int(nullable: false),
                        Location_LocationId = c.Guid(),
                        MotorVehicleModel_MotorVehicleModelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .ForeignKey("dbo.MotorVehicleModels", t => t.MotorVehicleModel_MotorVehicleModelId, cascadeDelete: true)
                .Index(t => t.Year)
                .Index(t => t.VIN)
                .Index(t => t.Registration)
                .Index(t => t.Location_LocationId)
                .Index(t => t.MotorVehicleModel_MotorVehicleModelId);
            
            CreateTable(
                "dbo.MotorVehicleModels",
                c => new
                    {
                        MotorVehicleModelId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        MotorVehicleType = c.Int(nullable: false),
                        FuelCapacity = c.Double(nullable: false),
                        FuelConsumption = c.Double(nullable: false),
                        EngineType = c.Int(nullable: false),
                        MaxPower = c.Int(nullable: false),
                        TransmissionType = c.Int(nullable: false),
                        AWD = c.Boolean(nullable: false),
                        CargoVolume = c.Int(nullable: false),
                        VehicleMake_VehicleMakeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MotorVehicleModelId)
                .ForeignKey("dbo.VehicleMakes", t => t.VehicleMake_VehicleMakeId, cascadeDelete: true)
                .Index(t => t.EngineType)
                .Index(t => t.MaxPower)
                .Index(t => t.TransmissionType)
                .Index(t => t.VehicleMake_VehicleMakeId);
            
            CreateTable(
                "dbo.VehicleMakes",
                c => new
                    {
                        VehicleMakeId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.VehicleMakeId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MotorVehicles", "MotorVehicleModel_MotorVehicleModelId", "dbo.MotorVehicleModels");
            DropForeignKey("dbo.MotorVehicleModels", "VehicleMake_VehicleMakeId", "dbo.VehicleMakes");
            DropForeignKey("dbo.MotorVehicles", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "Suburb_SuburbId", "dbo.Suburbs");
            DropForeignKey("dbo.Suburbs", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "State_StateId", "dbo.States");
            DropForeignKey("dbo.States", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.VehicleMakes", new[] { "Name" });
            DropIndex("dbo.MotorVehicleModels", new[] { "VehicleMake_VehicleMakeId" });
            DropIndex("dbo.MotorVehicleModels", new[] { "TransmissionType" });
            DropIndex("dbo.MotorVehicleModels", new[] { "MaxPower" });
            DropIndex("dbo.MotorVehicleModels", new[] { "EngineType" });
            DropIndex("dbo.MotorVehicles", new[] { "MotorVehicleModel_MotorVehicleModelId" });
            DropIndex("dbo.MotorVehicles", new[] { "Location_LocationId" });
            DropIndex("dbo.MotorVehicles", new[] { "Registration" });
            DropIndex("dbo.MotorVehicles", new[] { "VIN" });
            DropIndex("dbo.MotorVehicles", new[] { "Year" });
            DropIndex("dbo.Locations", new[] { "Address_AddressId" });
            DropIndex("dbo.Locations", new[] { "Name" });
            DropIndex("dbo.States", new[] { "Country_CountryId" });
            DropIndex("dbo.Cities", new[] { "State_StateId" });
            DropIndex("dbo.Suburbs", new[] { "City_CityId" });
            DropIndex("dbo.Addresses", new[] { "Suburb_SuburbId" });
            DropTable("dbo.VehicleMakes");
            DropTable("dbo.MotorVehicleModels");
            DropTable("dbo.MotorVehicles");
            DropTable("dbo.Locations");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Suburbs");
            DropTable("dbo.Addresses");
        }
    }
}
